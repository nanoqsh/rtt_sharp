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

        public Resource()
        {
            models = new Dictionary<string, Model>();
            tiles = new Dictionary<string, Tile>();
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

            Tile tile = TileConverter.Convert(Loader.LoadTile(file));
            tiles.Add(file, tile);
            return tile;
        }

        public List<Tile> LoadedTiles => tiles.Values.ToList();
    }
}
