using System.Collections.Generic;
using System.Linq;

namespace RT.Engine
{
    class Box
    {
        public Side Up { get; private set; }
        public Side Down { get; private set; }
        public Side Left { get; private set; }
        public Side Right { get; private set; }
        public Side Front { get; private set; }
        public Side Back { get; private set; }

        private delegate (float, float, float) PointTransform(float x, float y, float z);
        private readonly List<PointTransform> transforms;

        public Box()
        {
            Up = Side.Up;
            Down = Side.Down;
            Left = Side.Left;
            Right = Side.Right;
            Front = Side.Front;
            Back = Side.Back;

            transforms = new List<PointTransform>();
        }

        public Box Flip(Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    (Front, Back) = (Back, Front);
                    transforms.Add((x, y, z) => (-x, y, z));
                    break;

                case Axis.Y:
                    (Up, Down) = (Down, Up);
                    transforms.Add((x, y, z) => (x, -y, z));
                    break;

                case Axis.Z:
                    (Left, Right) = (Right, Left);
                    transforms.Add((x, y, z) => (x, y, -z));
                    break;

                default:
                    throw Undefined<Axis>.Error;
            };

            return this;
        }

        public Box Turn(Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    (Right, Down, Left, Up) = (Up, Right, Down, Left);
                    transforms.Add((x, y, z) => (x, -z, y));
                    break;

                case Axis.Y:
                    (Left, Back, Right, Front) = (Front, Left, Back, Right);
                    transforms.Add((x, y, z) => (z, y, -x));
                    break;

                case Axis.Z:
                    (Front, Down, Back, Up) = (Up, Front, Down, Back);
                    transforms.Add((x, y, z) => (-y, x, z));
                    break;

                default:
                    throw Undefined<Axis>.Error;
            };

            return this;
        }

        public (float, float, float) TransformPoint(float x, float y, float z) =>
            transforms.Aggregate((x, y, z), (p, t) => t(p.x, p.y, p.z));

        public override string ToString()
        {
            return $"{{ up: {Up}, down: {Down}, left: {Left}, right: {Right}, front: {Front}, back: {Back} }}";
        }
    }
}
