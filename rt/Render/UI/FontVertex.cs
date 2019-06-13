namespace RT.Render.UI
{
    public struct FontVertex
    {
        public float X;
        public float Y;
        public float U;
        public float V;

        public FontVertex(float x, float y, float u, float v)
        {
            X = x;
            Y = y;
            U = u;
            V = v;
        }

        public override string ToString() => $"({X}; {Y}; {U}; {V})";
    }
}
