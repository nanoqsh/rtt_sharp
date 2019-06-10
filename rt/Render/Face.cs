using OpenTK;
using RT.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using Point = RT.Engine.Point;

namespace RT.Render
{
    class Face
    {
        public Vertex[] Vertexes { get; private set; }
        public uint Layer { get; private set; }
        public Side Contact { get; private set; }
        public Vector3 Normal { get; private set; }

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

        public void UpdateTextureMap(Vector2[] textureMap)
        {
            for (int i = 0; i < Vertexes.Length; ++i)
                Vertexes[i].TextureMap = textureMap[i];
        }

        public Vector2[] TextureMap =>
            Vertexes.Select(v => v.TextureMap).ToArray();

        public Face(Face old, Point point)
        {
            Layer = old.Layer;
            Contact = old.Contact;
            Normal = old.Normal;

            Vertexes = new Vertex[old.Vertexes.Length];

            for (int i = 0; i < Vertexes.Length; ++i)
                Vertexes[i] = new Vertex(
                    point + old.Vertexes[i].Position,
                    old.Vertexes[i].TextureMap,
                    old.Vertexes[i].Normal
                    );
        }

        public Face(Face old, Box box)
        {
            Layer = old.Layer;
            Contact = old.Contact;
            Normal = old.Normal;

            Vertexes = new Vertex[old.Vertexes.Length];

            for (int i = 0; i < Vertexes.Length; ++i)
                Vertexes[i] = new Vertex(
                    box.TransformPoint(old.Vertexes[i].Position),
                    old.Vertexes[i].TextureMap,
                    old.Vertexes[i].Normal
                    );
        }
    }
}
