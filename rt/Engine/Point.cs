using OpenTK;
using System;

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

        public Point(Vector3 vector)
        {
            X = (int)(vector.X < 0 ? vector.X - 1 : vector.X);
            Y = (int)(vector.Y < 0 ? vector.Y - 1 : vector.Y);
            Z = (int)(vector.Z < 0 ? vector.Z - 1 : vector.Z);
        }

        public Point ChunkPoint =>
            new Point(
                X < 0 ? (X / Chunk.SIZE) - 1 : X / Chunk.SIZE,
                Y < 0 ? (Y / Chunk.SIZE) - 1 : Y / Chunk.SIZE,
                Z < 0 ? (Z / Chunk.SIZE) - 1 : Z / Chunk.SIZE
                );

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

        public Point UL => new Point(X + 1, Y + 1, Z);
        public Point UR => new Point(X - 1, Y + 1, Z);
        public Point DL => new Point(X + 1, Y - 1, Z);
        public Point DR => new Point(X - 1, Y - 1, Z);
        public Point FL => new Point(X + 1, Y, Z + 1);
        public Point FR => new Point(X - 1, Y, Z + 1);
        public Point BL => new Point(X + 1, Y, Z - 1);
        public Point BR => new Point(X - 1, Y, Z - 1);

        public Point ULF => new Point(X + 1, Y + 1, Z + 1);
        public Point ULB => new Point(X + 1, Y + 1, Z - 1);
        public Point URF => new Point(X - 1, Y + 1, Z + 1);
        public Point URB => new Point(X - 1, Y + 1, Z - 1);
        public Point DLF => new Point(X + 1, Y - 1, Z + 1);
        public Point DLB => new Point(X + 1, Y - 1, Z - 1);
        public Point DRF => new Point(X - 1, Y - 1, Z + 1);
        public Point DRB => new Point(X - 1, Y - 1, Z - 1);

        public double Distance(Point other) =>
            Math.Sqrt(
                  Math.Pow(X - other.X, 2)
                + Math.Pow(Y - other.Y, 2)
                + Math.Pow(Z - other.Z, 2)
                );

        public Point Mod(int m)
        {
            int x = X % m;
            int y = Y % m;
            int z = Z % m;

            return new Point(
                x < 0 ? x + m : x,
                y < 0 ? y + m : y,
                z < 0 ? z + m : z
                );
        }

        public override string ToString() => $"({X}, {Y}, {Z})";

        public override bool Equals(object obj)
        {
            if (obj is Point p)
                return X == p.X && Y == p.Y && Z == p.Z;

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X;
                hash = hash * 23 + Y;
                hash = hash * 23 + Z;
                return hash;
            }
        }
    }
}
