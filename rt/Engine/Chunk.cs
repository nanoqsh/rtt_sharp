using RT.Render;
using System.Collections.Generic;
using System.Linq;

namespace RT.Engine
{
    class Chunk
    {
        public const int SIZE = 16;

        public readonly Point Position;
        private readonly World world;
        private readonly Block?[,,] blocks;
        private Mesh? mesh = null;

        public Chunk(Point position, World world)
        {
            Position = position;
            this.world = world;
            blocks = new Block?[SIZE, SIZE, SIZE];
        }

        public Block GetBlock(Point point) =>
               (point.X < 0 || point.X >= SIZE)
            || (point.Y < 0 || point.Y >= SIZE)
            || (point.Z < 0 || point.Z >= SIZE)
            ? world.GetBlock(new Point(
                Position.X * SIZE + point.X,
                Position.Y * SIZE + point.Y,
                Position.Z * SIZE + point.Z
                ))
            : blocks[point.X, point.Y, point.Z] ?? Block.Empty;

        public void SetBlock(Point point, Tile tile, State? state = null)
        {
            blocks[point.X, point.Y, point.Z] = new Block(
                tile,
                state ?? tile.DetectState(this)
                );

            mesh = null;
        }

        public Mesh GetMesh(ShaderProgram shader) =>
            mesh ?? (mesh = new Mesh(BuildMesh(), shader));

        public void UpdateMesh() => mesh = null;

        private Face[] BuildMesh()
        {
            List<Face> faces = new List<Face>();

            for (int z = 0; z < SIZE; ++z)
                for (int y = 0; y < SIZE; ++y)
                    for (int x = 0; x < SIZE; ++x)
                    {
                        Block block = blocks[x, y, z] ?? Block.Empty;

                        if (block.IsEmpty)
                            continue;

                        Point point = new Point(x, y, z);

                        faces.AddRange(
                            Subtract(block.State, point)
                                .Select(f => new Face(f, point))
                            );
                    }

            return faces.ToArray();
        }

        private IEnumerable<Face> Subtract(State state, Point point)
        {
            foreach (Face face in state.Model.Faces)
            {
                Box box = state.Box;

                if (face.Contact.Contain(box.Right))
                {
                    Block block = GetBlock(point.Right);

                    if (!block.IsEmpty && block.State.Contain(block.State.Box.Left))
                        continue;
                }

                if (face.Contact.Contain(box.Left))
                {
                    Block block = GetBlock(point.Left);

                    if (!block.IsEmpty && block.State.Contain(block.State.Box.Right))
                        continue;
                }

                if (face.Contact.Contain(box.Down))
                {
                    Block block = GetBlock(point.Down);

                    if (!block.IsEmpty && block.State.Contain(block.State.Box.Up))
                        continue;
                }

                if (face.Contact.Contain(box.Up))
                {
                    Block block = GetBlock(point.Up);

                    if (!block.IsEmpty && block.State.Contain(block.State.Box.Down))
                        continue;
                }

                if (face.Contact.Contain(box.Back))
                {
                    Block block = GetBlock(point.Back);

                    if (!block.IsEmpty && block.State.Contain(block.State.Box.Front))
                        continue;
                }

                if (face.Contact.Contain(box.Front))
                {
                    Block block = GetBlock(point.Front);

                    if (!block.IsEmpty && block.State.Contain(block.State.Box.Back))
                        continue;
                }

                yield return face;
            }
        }
    }
}
