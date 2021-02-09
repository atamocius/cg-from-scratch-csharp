using SFML.Graphics;
using SFML.System;

namespace raytracer
{
    public readonly struct Sphere
    {
        public Vector3f Center { get; }
        public float Radius { get; }
        public Color Color { get; }

        public Sphere(Vector3f center, float radius, Color color)
        {
            this.Center = center;
            this.Radius = radius;
            this.Color = color;
        }
    }
}
