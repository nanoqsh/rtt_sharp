using OpenTK;
using RT.Engine;
using RT.Render;
using System;
using System.Drawing;

namespace RT
{
    class Program
    {
        static void Main(string[] args)
        {
            Core core = Core.Unit;
            // core.PreInit();
            // core.Init();

            // Model model = core.Resource.LoadModel("model.json");
            // Tile tile = core.Resource.LoadTile("tile_child.json");

            core.Atlas.LoadSprite("brick.png");
            core.Atlas.LoadSprite("stone.png");
            core.Atlas.LoadSprite("clay.png");
            core.Atlas.LoadSprite("dirt.png");
            //core.Atlas.LoadSprite("stonebrick.png");
            //core.Atlas.LoadSprite("trap.png");
            //core.Atlas.LoadSprite("trap.png");
            //core.Atlas.LoadSprite("grass.png");

            Vector2[] uvs = new Vector2[]
            {
                new Vector2(0, 1),
                new Vector2(0.5f, 0.5f),
                new Vector2(0.5f, 0),
                new Vector2(0, 0)
            };

            var map = core.Atlas.GlueSprites();
            var uv1 = map.GetUV(0, uvs);
            var uv2 = map.GetUV(1, uvs);
            var uv3 = map.GetUV(2, uvs);
            var uv4 = map.GetUV(3, uvs);

            Console.ReadKey();
        }
    }
}
