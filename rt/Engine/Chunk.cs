using RT.Render;
using System.Collections.Generic;
using System.Linq;

namespace RT.Engine
{
    class Chunk
    {
        public const int SIZE = 16;

        public readonly Point Position;
        private readonly Tile?[,,] tiles;
        private Mesh? mesh = null;

        public Chunk(Point position)
        {
            Position = position;
            tiles = new Tile[SIZE, SIZE, SIZE];
        }

        public Tile GetTile(Point point) =>
            tiles[point.X, point.Y, point.Z] ?? Tile.Empty;

        public void SetTile(Tile tile, Point point) =>
            tiles[point.X, point.Y, point.Z] = tile;

        public Mesh GetMesh(ShaderProgram shader) =>
            mesh ?? (mesh = new Mesh(BuildMesh(), shader));

        private Face[] BuildMesh()
        {
            List<Face> faces = new List<Face>();

            for (int z = 0; z < SIZE; ++z)
                for (int y = 0; y < SIZE; ++y)
                    for (int x = 0; x < SIZE; ++x)
                    {
                        Tile t = tiles[x, y, z] ?? Tile.Empty;

                        if (t == Tile.Empty || t.States.Length == 0)
                            continue;

                        State st = t.States[0];

                        foreach (Face face in st.Model.Faces)
                        {
                            if (x != 0 && face.Contact.Contain(Side.Back))
                            {
                                Tile n = GetTile(new Point(x - 1, y, z));
                                if (n.DefaultState.Model.FullSides.Contain(Side.Front))
                                    continue;
                            }

                            if (x != SIZE - 1 && face.Contact.Contain(Side.Front))
                            {
                                Tile n = GetTile(new Point(x + 1, y, z));
                                if (n.DefaultState.Model.FullSides.Contain(Side.Back))
                                    continue;
                            }

                            if (y != 0 && face.Contact.Contain(Side.Down))
                            {
                                Tile n = GetTile(new Point(x, y - 1, z));
                                if (n.DefaultState.Model.FullSides.Contain(Side.Up))
                                    continue;
                            }

                            if (y != SIZE - 1 && face.Contact.Contain(Side.Up))
                            {
                                Tile n = GetTile(new Point(x, y + 1, z));
                                if (n.DefaultState.Model.FullSides.Contain(Side.Down))
                                    continue;
                            }

                            if (z != 0 && face.Contact.Contain(Side.Right))
                            {
                                Tile n = GetTile(new Point(x, y, z - 1));
                                if (n.DefaultState.Model.FullSides.Contain(Side.Left))
                                    continue;
                            }

                            if (z != SIZE - 1 && face.Contact.Contain(Side.Left))
                            {
                                Tile n = GetTile(new Point(x, y, z + 1));
                                if (n.DefaultState.Model.FullSides.Contain(Side.Right))
                                    continue;
                            }

                            faces.Add(new Face(
                                face,
                                Core.Unit.SpriteMap.GetUV(
                                    st.Layers[face.Layer],
                                    face.Vertexes.Select(v => v.TextureMap).ToArray()
                                    ),
                                new Point(x, y, z)
                                ));
                        }
                    }

            return faces.ToArray();
        }
    }
}
