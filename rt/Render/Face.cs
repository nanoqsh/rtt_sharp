using OpenTK;
using RT.Engine;

namespace RT.Render
{
    class Face
    {
        public const int VERTEX_AMOUNT = 4;

        public readonly Vector3[] Positions;
        public readonly Vector2[] TextureMap;
        public readonly uint Layer;
        public readonly Side Contact;
        public readonly Vector3 Normal;

        public Face(Vector3[] positions, Vector2[] textureMap, uint layer, Side contact, Vector3? normal = null)
        {
            Positions = positions;
            TextureMap = textureMap;
            Layer = layer;
            Contact = contact;
            Normal = normal ?? Calculator.CalculateNormal(
                positions[0],
                positions[1],
                positions[2]
                );
        }
    }
}
