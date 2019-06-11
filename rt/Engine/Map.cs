using OpenTK;
using OpenTK.Input;
using System;

namespace RT.Engine
{
    class Map
    {
        private readonly Player player;
        public readonly Chunk Chunk;
        public readonly World World;

        public Map(Player player)
        {
            this.player = player;
            World = new World();
            Chunk = new Chunk(Point.Zero, World);
            World.AddChunk(Point.Zero, Chunk);
            World.AddChunk(new Point(-1, 0, 0));
            World.AddChunk(new Point(-1, 0, -1));
            World.AddChunk(new Point(0, 0, -1));
        }

        public void Start()
        {
            Tile tile = Core.Unit.Resource.LoadTile("stone.json");

            for (int x = -15; x < 16; ++x)
                for (int z = -15; z < 16; ++z)
                    World.SetBlock(new Point(x, 0, z), tile);

            World.SetBlock(new Point(15, 15, 15), tile);
            World.SetBlock(new Point(-15, 15, 15), tile);
            World.SetBlock(new Point(15, 15, -15), tile);
            World.SetBlock(new Point(-15, 15, -15), tile);
        }

        public void Update(float delta, Controller controller)
        {
            float speed = 1.4f;
            Vector3 move = new Vector3();

            if (controller.IsKeyPressed(controller.MoveForward))
                move += player.Camera.Front;

            if (controller.IsKeyPressed(controller.MoveBack))
                move -= player.Camera.Front;

            if (controller.IsKeyPressed(controller.MoveLeft))
                move -=
                    Vector3.Normalize(Vector3.Cross(player.Camera.Front, Vector3.UnitY));

            if (controller.IsKeyPressed(controller.MoveRight))
                move += 
                    Vector3.Normalize(Vector3.Cross(player.Camera.Front, Vector3.UnitY));

            if (controller.IsKeyPressed(controller.MoveUp))
                move += Vector3.UnitY;

            if (controller.IsKeyPressed(controller.MoveDown))
                move -= Vector3.UnitY;

            player.Move(move * speed * delta);
            player.Camera.Rotate(controller.MouseDelta);
        }

        public void DownKey(Key key, Controller controller)
        {
            if (key == controller.TogglePerspective)
                player.Camera.Perspective = !player.Camera.Perspective;

            if (key == Key.Q)
            {
                Point point = new Point(player.Position + player.Camera.Front);

                Tile tile = Core.Unit.Resource.LoadTile("brick.json");
                if (World.SetBlock(point, tile))
                    Console.WriteLine("Set on " + point);
                else
                    Console.WriteLine("NOT Set on " + point);
            }
        }
    }
}
