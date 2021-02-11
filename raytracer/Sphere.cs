using SFML.Graphics;
using SFML.System;

namespace raytracer
{
    public readonly struct Sphere
    {
        public Vector3f Center { get; init; }
        public float Radius { get; init; }
        public Color Color { get; init; }
    }
}
