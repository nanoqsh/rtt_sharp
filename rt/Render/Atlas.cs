using System.Collections.Generic;

namespace RT.Render
{
    class Atlas
    {
        private readonly Dictionary<string, uint> ids;
        private readonly List<string> sprites;

        public Atlas()
        {
            ids = new Dictionary<string, uint>();
            sprites = new List<string>();
        }

        public uint LoadSprite(string file)
        {
            if (ids.TryGetValue(file, out uint value))
                return value;

            uint id = (uint)sprites.Count;
            ids.Add(file, id);
            sprites.Add(file);
            return id;
        }
    }
}
