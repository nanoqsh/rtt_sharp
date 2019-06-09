using OpenTK;

namespace RT.Engine
{
    struct Point
    {
        public int X;
        public int Y;
        public int Z;

        public Point(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static readonly Point Zero = new Point(0, 0, 0);

        public static Vector3 operator +(Point point, Vector3 vector) =>
            new Vector3(
                vector.X + point.X,
                vector.Y + point.Y,
                vector.Z + point.Z
                );
    }
}
