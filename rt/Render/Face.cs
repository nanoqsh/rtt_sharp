using OpenTK;

namespace RT.Render
{
    class Face
    {
        public const int VERTEX_AMOUNT = 4;

        public readonly Vector3[] Positions;
        public readonly Vector2[] TextureMap;
        public readonly Vector3[] Normals;

        public Face(Vector3[] positions, Vector2[] textureMap, Vector3[]? normals = null)
        {
            Positions = positions;
            TextureMap = textureMap;

            if (normals == null)
            {
                Vector3 normal = Calculator.CalculateNormal(
                    positions[0],
                    positions[1],
                    positions[2]
                    );

                normals = new Vector3[] { normal, normal, normal, normal };
            }

            Normals = normals;
        }
    }
}
