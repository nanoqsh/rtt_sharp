using RT.Render;
using System;

namespace RT
{
    class Program
    {
        static void Main(string[] args)
        {
            Core core = Core.Inst;

            Model model = core.Resource.LoadModel("model.json");

            Scheme.Tile tile = Loader.LoadTile("tile_child.json");

            Console.ReadKey();
        }
    }
}
