using OpenTK;
using RT.Engine;
using System;

namespace RT.Render
{
    class Face
    {
        public readonly Vertex[] Vertexes;
        public readonly uint Layer;
        public readonly Side Contact;
        public readonly Vector3 Normal;

        public Face(Vector3[] positions, Vector2[] textureMap, uint layer, Side contact, Vector3? normal = null)
        {
            Layer = layer;
            Contact = contact;

            if (!((positions.Length == 3 && textureMap.Length == 3) || (positions.Length == 4 && textureMap.Length == 4)))
                throw new Exception("Count of position vectors and texture map vectors must be 3 or 4");

            Normal = normal ?? Calculator.CalculateNormal(
                positions[0],
                positions[1],
                positions[2]
                );

            int len = positions.Length;
            Vertexes = new Vertex[len];

            for (int i = 0; i < len; ++i)
                Vertexes[i] = new Vertex(
                    positions[i],
                    textureMap[i],
                    Normal
                    );
        }
    }
}
