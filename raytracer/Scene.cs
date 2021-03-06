using System;
using SFML.Graphics;
using SFML.System;
using Atamocius.Core;

namespace raytracer
{
    public class Scene
    {
        private static readonly Vector2f VIEWPORT_SIZE = new(1, 1);
        private const float PROJECTION_PLANE_Z = 1;
        private static readonly Vector3f CAMERA_POSITION = new(0, 0, 0);
        private static readonly Color BACKGROUND_COLOR = Color.White;
        private static readonly Sphere[] SPHERES =
        {
            new Sphere
            {
                Center = new (0, -1, 3),
                Radius = 1,
                Color = Color.Red
            },
            new Sphere
            {
                Center = new (2, 0, 4),
                Radius = 1,
                Color = Color.Blue
            },
            new Sphere
            {
                Center = new (-2, 0, 4),
                Radius = 1,
                Color = Color.Green
            },
        };

        private readonly ICanvas canvas;

        public Scene(ICanvas canvas)
        {
            this.canvas = canvas;
        }

        public void Trace()
        {
            var (cw, ch) = this.canvas.Size;

            var (minX, maxX) = (-cw / 2, cw / 2);
            var (minY, maxY) = (-ch / 2, ch / 2);

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    var direction = this.CanvasToViewport(new(x, y));

                    var color = TraceRay(
                        CAMERA_POSITION,
                        direction,
                        1,
                        float.PositiveInfinity,
                        SPHERES,
                        BACKGROUND_COLOR);

                    this.canvas.PutPixel(x, y, color);
                }
            }
        }

        /// <summary>
        /// Converts 2D canvas coordinates to 3D viewport coordinates.
        /// </summary>
        private Vector3f CanvasToViewport(in Vector2i canvasPos) =>
             new(
                canvasPos.X * VIEWPORT_SIZE.X / this.canvas.Size.Width,
                canvasPos.Y * VIEWPORT_SIZE.Y / this.canvas.Size.Height,
                PROJECTION_PLANE_Z);

        /// <summary>
        /// Traces a ray against the set of spheres in the scene.
        /// </summary>
        private static Color TraceRay(
            in Vector3f origin,
            in Vector3f direction,
            in float minT,
            in float maxT,
            in Sphere[] spheres,
            in Color backgroundColor)
        {
            var closestT = float.PositiveInfinity;
            Sphere? closestSphere = null;

            for (var i = 0; i < spheres.Length; i++)
            {
                Vector2f ts = IntersectRaySphere(
                    origin,
                    direction,
                    spheres[i]);

                if (ts.X < closestT && minT < ts.X && ts.X < maxT)
                {
                    closestT = ts.X;
                    closestSphere = spheres[i];
                }
                if (ts.Y < closestT && minT < ts.Y && ts.Y < maxT)
                {
                    closestT = ts.Y;
                    closestSphere = spheres[i];
                }
            }

            if (closestSphere == null)
            {
                return backgroundColor;
            }

            return closestSphere.Value.Color;
        }

        /// <summary>
        /// Computes the intersection of a ray and a sphere. Returns the values
        /// of t for the intersections.
        /// </summary>
        public static Vector2f IntersectRaySphere(
            in Vector3f origin,
            in Vector3f direction,
            in Sphere sphere)
        {
            var co = origin - sphere.Center;

            var k1 = MathHelper.DotProduct(direction, direction);
            var k2 = 2 * MathHelper.DotProduct(co, direction);
            var k3 =
                MathHelper.DotProduct(co, co) - sphere.Radius * sphere.Radius;

            var discriminant = k2 * k2 - 4 * k1 * k3;
            if (discriminant < 0)
            {
                return new(float.PositiveInfinity, float.PositiveInfinity);
            }

            var t1 = (-k2 + MathF.Sqrt(discriminant)) / (2 * k1);
            var t2 = (-k2 - MathF.Sqrt(discriminant)) / (2 * k1);

            return new(t1, t2);
        }
    }
}
