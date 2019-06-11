namespace RT.Engine
{
    enum Edge
    {
        None  = 0,
        Up    = 1 << 0,
        Down  = 1 << 1,
        Left  = 1 << 2,
        Right = 1 << 3
    }

    static class EdgeMethods
    {
        public static Corner GetAdjacentCorners(this Edge edge) => edge switch
        {
            Edge.Up    => Corner.UpLeft   | Corner.UpRight,
            Edge.Down  => Corner.DownLeft | Corner.DownRight,
            Edge.Left  => Corner.UpLeft   | Corner.DownLeft,
            Edge.Right => Corner.UpRight  | Corner.DownRight,
            _          => Corner.None
        };
    }
}
