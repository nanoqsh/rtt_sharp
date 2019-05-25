using OpenTK;

namespace RT.Render
{
    static class Calculator
    {
        public static Vector3 CalculateNormal(Vector3 a, Vector3 b, Vector3 c) =>
            Vector3.Normalize(Vector3.Cross(b - a, c - a));
    }
}
