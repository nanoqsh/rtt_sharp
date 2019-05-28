using System.Collections.Generic;

namespace RT.Engine
{
    struct Location
    {
        public Point Point;
        public Side Side;
    }

    class CoverData
    {
        private class Data
        {
            public Cover Cover;
            public Edge Edges;
            public Edge Points;

            public Data(Cover cover)
            {
                Cover = cover;
            }
        }

        private readonly Dictionary<Location, List<Data>> covers;
        
        public CoverData()
        {
            covers = new Dictionary<Location, List<Data>>();
        }

        public void AddEdge(Location location, Cover cover, Edge edge)
        {
            Data data = At(location, cover);
            data.Edges |= edge;
        }

        public void AddPoint(Location location, Cover cover, Edge edge)
        {
            Data data = At(location, cover);
            data.Points |= edge;
        }

        private Data At(Location location, Cover cover)
        {
            Data result;

            if (covers.TryGetValue(location, out List<Data> value))
            {
                Data data = value.Find(d => d.Cover == cover);

                if (data != null)
                    return data;

                result = new Data(cover);
                covers[location].Add(result);
                return result;
            }

            result = new Data(cover);
            covers.Add(location, new List<Data> { result });
            return result;
        }
    }
}
