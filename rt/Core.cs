using RT.Engine;

namespace RT
{
    class Core
    {
        // public readonly Window Window;
        public readonly Player Player;
        public readonly Map Map;
        public readonly Resource Resource;

        public static Core Unit = new Core();

        public Core()
        {
            // Window = new Window(800, 600, "RTE");
            Player = new Player("nekosora");
            Map = new Map();
            Resource = new Resource();
        }

        public void Init()
        {
            // Window.Run(60);
        }
    }
}
