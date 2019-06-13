namespace RT.Render
{
    class Quad
    {
        public static readonly Vertex4[] Data = new Vertex4[]
        {
            new Vertex4(-1.0f,  1.0f,  0.0f,  1.0f),
            new Vertex4(-1.0f, -1.0f,  0.0f,  0.0f),
            new Vertex4( 1.0f,  1.0f,  1.0f,  1.0f),
            new Vertex4( 1.0f, -1.0f,  1.0f,  0.0f)
        };
    }
}
