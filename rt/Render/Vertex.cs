using OpenTK;

namespace RT.Render
{
    struct Vertex
    {
        public readonly Vector3 Position;
        public readonly Vector2 TextureMap;
        public readonly Vector3 Normal;

        public Vertex(Vector3 position, Vector2 textureMap, Vector3 normal)
        {
            Position = position;
            TextureMap = textureMap;
            Normal = normal;
        }

        public static Attribute[] GetAttributes(ShaderProgram shader) =>
            new Attribute[] {
                new Attribute(shader.GetAttributeIndex("position"), 3, 8, 0),
                new Attribute(shader.GetAttributeIndex("texture_map"), 2, 8, 3),
                new Attribute(shader.GetAttributeIndex("normal"), 3, 8, 5)
            };
    }
}
