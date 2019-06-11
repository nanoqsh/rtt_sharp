using OpenTK;
using RT.Render;
using System.Collections.Generic;

namespace RT.Engine
{
    class World
    {
        private readonly Dictionary<Point, Chunk> chunks;

        public World()
        {
            chunks = new Dictionary<Point, Chunk>();
        }

        public void AddChunk(Point point, Chunk chunk)
        {
            chunks.Add(point, chunk);
        }

        public void AddChunk(Point point)
        {
            chunks.Add(point, new Chunk(point, this));
        }

        public bool SetBlock(Point point, Tile tile, State? state = null)
        {
            if (chunks.TryGetValue(point.ChunkPoint, out Chunk chunk))
            {
                Point loc = point.Mod(Chunk.SIZE);
                chunk.SetBlock(loc, tile, state);

                if (loc.X == 0) UpdateMesh(point.Right);
                if (loc.X == Chunk.SIZE - 1) UpdateMesh(point.Left);

                if (loc.Y == 0) UpdateMesh(point.Down);
                if (loc.Y == Chunk.SIZE - 1) UpdateMesh(point.Up);

                if (loc.Z == 0) UpdateMesh(point.Back);
                if (loc.Z == Chunk.SIZE - 1) UpdateMesh(point.Front);

                return true;
            }

            return false;
        }

        public void UpdateMesh(Point point)
        {
            if (chunks.TryGetValue(point.ChunkPoint, out Chunk chunk))
                chunk.UpdateMesh();
        }

        public Block GetBlock(Point point)
        {
            if (chunks.TryGetValue(point.ChunkPoint, out Chunk chunk))
                return chunk.GetBlock(point.Mod(Chunk.SIZE));

            return Block.Empty;
        }

        public List<(Mesh, Vector3)> GetMeshes(ShaderProgram shader)
        {
            List<(Mesh, Vector3)> result = new List<(Mesh, Vector3)>();

            foreach (Chunk chunk in chunks.Values)
                result.Add((chunk.GetMesh(shader), new Vector3(
                    chunk.Position.X * Chunk.SIZE + 0.5f,
                    chunk.Position.Y * Chunk.SIZE + 0.5f,
                    chunk.Position.Z * Chunk.SIZE + 0.5f
                    )));

            return result;
        }
    }
}
