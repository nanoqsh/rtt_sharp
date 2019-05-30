using RT.Engine;
using RT.Render;
using System;

namespace RT
{
    class Program
    {
        static void Main(string[] args)
        {
            Core core = Core.Unit;

            Model model = core.Resource.LoadModel("model.json");
            Tile tile = core.Resource.LoadTile("tile_child.json");

            Console.ReadKey();
        }
    }
}
