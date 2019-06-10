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

        public Tile GetTile(Point point)
        {
            int size = Chunk.SIZE;
            Point chunkPoint = new Point(point.X / size, point.Y / size, point.Z / size);

            if (chunks.TryGetValue(chunkPoint, out Chunk value))
                return value.GetTile(new Point(point.X % size, point.Y % size, point.Z % size));

            return Tile.Empty;
        }
    }
}
