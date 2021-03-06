﻿using OpenTK;
using RT.Engine;
using RT.Render;

namespace RT
{
    class Core
    {
        public readonly Window Window;
        public readonly Player Player;
        public readonly Map Map;
        public readonly Resource Resource;
        public readonly Atlas Atlas;

        private SpriteMap? spriteMap;
        public SpriteMap SpriteMap =>
            spriteMap ?? (spriteMap = Atlas.GlueSprites());

        public static Core Unit = new Core();

        public Core()
        {
            Window = new Window(900, 600, "RTE");
            Player = new Player("nekosora", new Vector3(0, 2, 0));
            Map = new Map(Player);
            Resource = new Resource();
            Atlas = new Atlas(16);
        }

        public void PreInit()
        {
            Resource.LoadTile("brick.json");
            Resource.LoadTile("pyramid.json");
            Resource.LoadTile("debug.json");
        }

        public void Init()
        {
            Window.Run(60);
        }
    }
}
