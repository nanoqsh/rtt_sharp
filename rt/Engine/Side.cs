namespace RT.Engine
{
    enum Side
    {
        None  = 0,
        Up    = 1 << 0,
        Down  = 1 << 1,
        Left  = 1 << 2,
        Right = 1 << 3,
        Front = 1 << 4,
        Back  = 1 << 5
    }

    static class SideMethods
    {
        public static Side GetOpposite(this Side side) => side switch
        {
            Side.Up    => Side.Down,
            Side.Down  => Side.Up,
            Side.Left  => Side.Right,
            Side.Right => Side.Left,
            Side.Front => Side.Back,
            Side.Back  => Side.Front,
            _          => throw Undefined<Side>.Error
        };

        public static Side FromString(string text)
        {
            Side result = Side.None;

            foreach (char c in text.ToLower())
                result |= c switch
                {
                    'u' => Side.Up,
                    'd' => Side.Down,
                    'l' => Side.Left,
                    'r' => Side.Right,
                    'f' => Side.Front,
                    'b' => Side.Back,
                    _   => Side.None
                };

            return result;
        }
    }
}
