using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RT.Exceptions;
using RT.Scheme;
using RT.Scheme.Requirements;
using System.Collections.Generic;
using System.IO;

namespace RT
{
    static class Loader
    {
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Error,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public static string LoadText(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File {path} not exists!");

            return File.ReadAllText(path);
        }

        public static Model LoadModel(string file)
        {
            Model model = JsonConvert.DeserializeObject<Model>(
                LoadText(Ref.Models + file),
                settings
                );

            List<string> errors = ModelRequirements.Check(model);
            if (errors.Count != 0)
                throw new LoaderException(file, errors);

            return model;
        }

        public static Tile LoadTile(string file)
        {
            Tile tile = LoadTileParent(file, new List<string>());

            List<string> errors = TileRequirements.Check(tile);
            if (errors.Count != 0)
                throw new LoaderException(file, errors);

            return tile;
        }

        private static Tile LoadTileParent(string file, List<string> loaded)
        {
            if (loaded.Contains(file))
                throw new LoaderException(file, new List<string> {
                    $"Recursive inheritance: {string.Join(" -> ", loaded)} -> {file}"
                });

            Tile tile = JsonConvert.DeserializeObject<Tile>(
                LoadText(Ref.Tiles + file),
                settings
                );

            loaded.Add(file);
            if (tile.Parent != null)
                tile = Tile.Inherit(tile, LoadTileParent(tile.Parent, loaded));

            return tile;
        }
    }
}
