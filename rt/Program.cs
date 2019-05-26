using RT.Engine;
using RT.Scheme;
using RT.Scheme.Converters;
using System;

namespace RT
{
    class Program
    {
        static void Main(string[] args)
        {
            // new Core().Init();

            // Tile tile = Loader.LoadTile("tile_child.json");

            Model model = Loader.LoadModel("model.json");
            Render.Model renderModel = ModelConverter.Convert(model);

            Console.ReadKey();
        }
    }
}
