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

        public Box()
        {
            Up = Side.Up;
            Down = Side.Down;
            Left = Side.Left;
            Right = Side.Right;
            Front = Side.Front;
            Back = Side.Back;
        }

        public Box Flip(Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    (Front, Back) = (Back, Front);
                    break;

                case Axis.Y:
                    (Up, Down) = (Down, Up);
                    break;

                case Axis.Z:
                    (Left, Right) = (Right, Left);
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
                    break;

                case Axis.Y:
                    (Left, Back, Right, Front) = (Front, Left, Back, Right);
                    break;

                case Axis.Z:
                    (Front, Down, Back, Up) = (Up, Front, Down, Back);
                    break;

                default:
                    throw Undefined<Axis>.Error;
            };

            return this;
        }

        public override string ToString()
        {
            return $"{{ up: {Up}, down: {Down}, left: {Left}, right: {Right}, front: {Front}, back: {Back} }}";
        }
    }
}
