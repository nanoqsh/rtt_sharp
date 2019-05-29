using System.Collections.Generic;

namespace RT.Engine
{
    class CoverData
    {
        private struct Location
        {
            public Point Point;
            public Side Side;
            public Cover Cover;

            public Location(Point point, Side side, Cover cover)
            {
                Point = point;
                Side = side;
                Cover = cover;
            }
        }

        private class Data
        {
            public Edge Edges;
            public Corner Corners;
        }

        private readonly Dictionary<Location, Data> covers;
        
        public CoverData()
        {
            covers = new Dictionary<Location, Data>();
        }

        public void AddCover(Point point, Side side, Cover cover, Edge edge)
        {
            Data data = At(point, side, cover);
            data.Edges |= edge;
            data.Corners &= ~data.Edges.GetAdjacentCorners();
        }

        public void AddCover(Point point, Side side, Cover cover, Corner corner)
        {
            Data data = At(point, side, cover);
            if (!corner.Contain(data.Edges.GetAdjacentCorners()))
                data.Corners |= corner;
        }

        private Data At(Point point, Side side, Cover cover)
        {
            Location location = new Location(point, side, cover);

            if (covers.TryGetValue(location, out Data value))
                return value;

            Data data = new Data();
            covers.Add(location, data);
            return data;
        }
    }
}
