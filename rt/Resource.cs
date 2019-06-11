using RT.Engine;
using RT.Render;
using RT.Scheme.Converters;
using System.Collections.Generic;
using System.Linq;

namespace RT
{
    class Resource
    {
        private readonly Dictionary<string, Model> models;
        private readonly Dictionary<string, Tile> tiles;
        private readonly List<Tile> tilesByID;

        public Resource()
        {
            models = new Dictionary<string, Model>();
            tiles = new Dictionary<string, Tile>();
            tilesByID = new List<Tile>()
            {
                Tile.Empty
            };
        }

        public Model LoadModel(string file)
        {
            if (models.TryGetValue(file, out Model result))
                return result;

            Model model = ModelConverter.Convert(Loader.LoadModel(file));
            models.Add(file, model);
            return model;
        }

        public Tile LoadTile(string file)
        {
            if (tiles.TryGetValue(file, out Tile result))
                return result;

            uint id = (uint)tilesByID.Count();
            Tile tile = TileConverter.Convert(Loader.LoadTile(file), id);
            tiles.Add(file, tile);
            tilesByID.Add(tile);
            return tile;
        }

        public List<Tile> LoadedTiles => tiles.Values.ToList();

        public Tile GetTile(uint id) =>
            tilesByID.Count() > ((int)id) ? tilesByID[(int)id] : Tile.Empty;
    }
}
