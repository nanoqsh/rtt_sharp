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
            IEnumerable<Face> Subtract(IEnumerable<Face> faces, Box box, Point pt)
            {
                foreach (Face face in faces)
                {
                    if (pt.X != 0 && face.Contact.Contain(box.Right))
                    {
                        Tile n = GetTile(new Point(pt.X - 1, pt.Y, pt.Z));

                        if (n != Tile.Empty && n.DefaultState.Model.FullSides.Contain(n.DefaultState.Box.Left))
                            continue;
                    }

                    if (pt.X != SIZE - 1 && face.Contact.Contain(box.Left))
                    {
                        Tile n = GetTile(new Point(pt.X + 1, pt.Y, pt.Z));

                        if (n != Tile.Empty && n.DefaultState.Model.FullSides.Contain(n.DefaultState.Box.Right))
                            continue;
                    }

                    if (pt.Y != 0 && face.Contact.Contain(box.Down))
                    {
                        Tile n = GetTile(new Point(pt.X, pt.Y - 1, pt.Z));

                        if (n != Tile.Empty && n.DefaultState.Model.FullSides.Contain(n.DefaultState.Box.Up))
                            continue;
                    }

                    if (pt.Y != SIZE - 1 && face.Contact.Contain(box.Up))
                    {
                        Tile n = GetTile(new Point(pt.X, pt.Y + 1, pt.Z));

                        if (n != Tile.Empty && n.DefaultState.Model.FullSides.Contain(n.DefaultState.Box.Down))
                            continue;
                    }

                    if (pt.Z != 0 && face.Contact.Contain(box.Back))
                    {
                        Tile n = GetTile(new Point(pt.X, pt.Y, pt.Z - 1));

                        if (n != Tile.Empty && n.DefaultState.Model.FullSides.Contain(n.DefaultState.Box.Front))
                            continue;
                    }

                    if (pt.Z != SIZE - 1 && face.Contact.Contain(box.Front))
                    {
                        Tile n = GetTile(new Point(pt.X, pt.Y, pt.Z + 1));

                        if (n != Tile.Empty && n.DefaultState.Model.FullSides.Contain(n.DefaultState.Box.Back))
                            continue;
                    }

                    yield return face;
                }
            }

            List<Face> faces = new List<Face>();

            for (int z = 0; z < SIZE; ++z)
                for (int y = 0; y < SIZE; ++y)
                    for (int x = 0; x < SIZE; ++x)
                    {
                        Tile t = tiles[x, y, z] ?? Tile.Empty;

                        if (t == Tile.Empty || t.States.Length == 0)
                            continue;

                        faces.AddRange(Subtract(t.DefaultState.Model.Faces, t.DefaultState.Box, new Point(x, y, z))
                            .Select(f => new Face(
                                f,
                                Core.Unit.SpriteMap.GetUV(
                                    t.DefaultState.Layers[f.Layer],
                                    f.Vertexes.Select(v => v.TextureMap).ToArray()
                                    ),
                                new Point(x, y, z)
                                )));
                    }

            return faces.ToArray();
        }
    }
}
