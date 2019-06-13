namespace RT.Render
{
    struct Vertex4
    {
        public float X;
        public float Y;
        public float U;
        public float V;

        public Vertex4(float x, float y, float u, float v)
        {
            X = x;
            Y = y;
            U = u;
            V = v;
        }

        public static Attribute GetAttribute(ShaderProgram shader, string name) =>
            new Attribute(shader.GetAttributeIndex(name), 4, 4, 0);

        public override string ToString() => $"({X}; {Y}; {U}; {V})";
    }
}
