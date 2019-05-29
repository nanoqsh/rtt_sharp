namespace RT.Engine
{
    enum Corner
    {
        None      = 0,
        UpLeft    = 1 << 0,
        UpRight   = 1 << 1,
        DownLeft  = 1 << 2,
        DownRight = 1 << 3
    }

    static class CornerMethods
    {
        public static bool Contain(this Corner corner, Corner other) =>
            (corner & other) == other;
    }
}
