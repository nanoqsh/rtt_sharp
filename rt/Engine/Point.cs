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

        public static Vector3 operator *(Point point, Vector3 vector) =>
            new Vector3(
                vector.X * point.X,
                vector.Y * point.Y,
                vector.Z * point.Z
                );

        public static Vector3 operator +(Point point, Point other) =>
            new Vector3(
                other.X + point.X,
                other.Y + point.Y,
                other.Z + point.Z
                );

        public static Vector3 operator *(Point point, Point other) =>
            new Vector3(
                other.X * point.X,
                other.Y * point.Y,
                other.Z * point.Z
                );

        public Point Up => new Point(X, Y + 1, Z);
        public Point Down => new Point(X, Y - 1, Z);
        public Point Left => new Point(X + 1, Y, Z);
        public Point Right => new Point(X - 1, Y, Z);
        public Point Front => new Point(X, Y, Z + 1);
        public Point Back => new Point(X, Y, Z - 1);
    }
}
