using SFML.System;

namespace Atamocius.Core
{
    public static class MathHelper
    {
        /// <summary>
        /// Dot product of two 3D vectors.
        /// </summary>
        public static float DotProduct(in Vector3f v1, in Vector3f v2) =>
            v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
    }
}
