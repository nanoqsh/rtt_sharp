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

        public void SetTile(Tile tile, Point point)
        {
            tiles[point.X, point.Y, point.Z] = tile;
            mesh = null;
        }

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

                        faces.AddRange(Subtract(t.DefaultState.Model.Faces, t.DefaultState.Box, new Point(x, y, z))
                            .Select(f => new Face(f, new Point(x, y, z))));
                    }

            return faces.ToArray();
        }

        private IEnumerable<Face> Subtract(IEnumerable<Face> faces, Box box, Point point)
        {
            foreach (Face face in faces)
            {
                if (point.X != 0 && face.Contact.Contain(box.Right))
                {
                    Tile n = GetTile(point.Right);

                    if (n != Tile.Empty && n.DefaultState.Model.FullSides.Contain(n.DefaultState.Box.Left))
                        continue;
                }

                if (point.X != SIZE - 1 && face.Contact.Contain(box.Left))
                {
                    Tile n = GetTile(point.Left);

                    if (n != Tile.Empty && n.DefaultState.Model.FullSides.Contain(n.DefaultState.Box.Right))
                        continue;
                }

                if (point.Y != 0 && face.Contact.Contain(box.Down))
                {
                    Tile n = GetTile(point.Down);

                    if (n != Tile.Empty && n.DefaultState.Model.FullSides.Contain(n.DefaultState.Box.Up))
                        continue;
                }

                if (point.Y != SIZE - 1 && face.Contact.Contain(box.Up))
                {
                    Tile n = GetTile(point.Up);

                    if (n != Tile.Empty && n.DefaultState.Model.FullSides.Contain(n.DefaultState.Box.Down))
                        continue;
                }

                if (point.Z != 0 && face.Contact.Contain(box.Back))
                {
                    Tile n = GetTile(point.Back);

                    if (n != Tile.Empty && n.DefaultState.Model.FullSides.Contain(n.DefaultState.Box.Front))
                        continue;
                }

                if (point.Z != SIZE - 1 && face.Contact.Contain(box.Front))
                {
                    Tile n = GetTile(point.Front);

                    if (n != Tile.Empty && n.DefaultState.Model.FullSides.Contain(n.DefaultState.Box.Back))
                        continue;
                }

                yield return face;
            }
        }
    }
}
