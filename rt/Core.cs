using RT.Engine;
using RT.Render;

namespace RT
{
    class Core
    {
        // public readonly Window Window;
        public readonly Player Player;
        public readonly Map Map;
        public readonly Resource Resource;
        public readonly Atlas Atlas;

        public static Core Unit = new Core();

        public Core()
        {
            // Window = new Window(800, 600, "RTE");
            Player = new Player("nekosora");
            Map = new Map();
            Resource = new Resource();
            Atlas = new Atlas();
        }

        public void Init()
        {
            // Window.Run(60);
        }
    }
}
