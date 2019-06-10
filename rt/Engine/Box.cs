using OpenTK;
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

        private delegate Vector3 PointTransform(Vector3 point);
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
                    (Right, Left) = (Left, Right);
                    transforms.Add(p => new Vector3(-p.X, p.Y, p.Z));
                    break;

                case Axis.Y:
                    (Up, Down) = (Down, Up);
                    transforms.Add(p => new Vector3(p.X, -p.Y, p.Z));
                    break;

                case Axis.Z:
                    (Back, Front) = (Front, Back);
                    transforms.Add(p => new Vector3(p.X, p.Y, -p.Z));
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
                    (Front, Down, Back, Up) = (Up, Front, Down, Back);
                    transforms.Add(p => new Vector3(p.X, -p.Z, p.Y));
                    break;

                case Axis.Y:
                    (Left, Back, Right, Front) = (Front, Left, Back, Right);
                    transforms.Add(p => new Vector3(p.Z, p.Y, -p.X));
                    break;

                case Axis.Z:
                    (Right, Down, Left, Up) = (Up, Right, Down, Left);
                    transforms.Add(p => new Vector3(-p.Y, p.X, p.Z));
                    break;

                default:
                    throw Undefined<Axis>.Error;
            };

            return this;
        }

        public Vector3 TransformPoint(Vector3 point) =>
            transforms.Aggregate(point, (p, t) => t(p));

        public override string ToString()
        {
            return $"{{ up: {Up}, down: {Down}, left: {Left}, right: {Right}, front: {Front}, back: {Back} }}";
        }
    }
}
