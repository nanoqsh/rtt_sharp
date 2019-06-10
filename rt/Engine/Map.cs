using OpenTK;
using OpenTK.Input;

namespace RT.Engine
{
    class Map
    {
        private readonly Player player;
        public readonly Chunk Chunk;

        public Map(Player player)
        {
            this.player = player;
            Chunk = new Chunk(Point.Zero);
        }

        public void Start()
        {
            Tile tile = Core.Unit.Resource.LoadTile("brick.json");
            Chunk.SetTile(tile, new Point(0, 0, 0));
            Chunk.SetTile(tile, new Point(0, 1, 0));
            Chunk.SetTile(tile, new Point(0, 0, 1));
            Chunk.SetTile(tile, new Point(0, 1, 1));
            Chunk.SetTile(tile, new Point(1, 0, 0));
            Chunk.SetTile(tile, new Point(1, 0, 1));
            Chunk.SetTile(tile, new Point(1, 1, 1));

            Chunk.SetTile(tile, new Point(3, 3, 3));
            Chunk.SetTile(tile, new Point(3, 4, 3));
            Chunk.SetTile(tile, new Point(3, 5, 3));
            Chunk.SetTile(tile, new Point(3, 3, 4));
            Chunk.SetTile(tile, new Point(3, 3, 5));
            Chunk.SetTile(tile, new Point(4, 3, 5));
            Chunk.SetTile(tile, new Point(3, 4, 4));
            Chunk.SetTile(tile, new Point(2, 5, 4));
            Chunk.SetTile(tile, new Point(2, 4, 5));
            Chunk.SetTile(tile, new Point(2, 5, 5));
            Chunk.SetTile(tile, new Point(2, 6, 4));
            Chunk.SetTile(tile, new Point(2, 7, 4));

            Chunk.SetTile(tile, new Point(3, 1, 1));

            Chunk.SetTile(tile, new Point(5, 1, 4));
            Chunk.SetTile(tile, new Point(5, 2, 4));
            Chunk.SetTile(tile, new Point(5, 2, 3));
            Chunk.SetTile(tile, new Point(5, 2, 5));
            Chunk.SetTile(tile, new Point(5, 3, 3));
            Chunk.SetTile(tile, new Point(5, 3, 5));

            Chunk.SetTile(tile, new Point(7, 4, 4));
            Chunk.SetTile(tile, new Point(7, 4, 5));
            Chunk.SetTile(tile, new Point(8, 4, 4));
            Chunk.SetTile(tile, new Point(8, 4, 5));
            Chunk.SetTile(tile, new Point(7, 4, 6));
            Chunk.SetTile(tile, new Point(7, 4, 7));
            Chunk.SetTile(tile, new Point(8, 4, 6));
            Chunk.SetTile(tile, new Point(8, 4, 7));
            Chunk.SetTile(tile, new Point(9, 4, 4));
            Chunk.SetTile(tile, new Point(9, 4, 5));
            Chunk.SetTile(tile, new Point(10, 4, 4));
            Chunk.SetTile(tile, new Point(10, 4, 5));
            Chunk.SetTile(tile, new Point(9, 4, 6));
            Chunk.SetTile(tile, new Point(9, 4, 7));
            Chunk.SetTile(tile, new Point(10, 4, 6));
            Chunk.SetTile(tile, new Point(10, 4, 7));

            Chunk.SetTile(tile, new Point(15, 15, 15));

            Chunk.SetTile(tile, new Point(15, 0, 0));

            Chunk.SetTile(tile, new Point(15, 0, 15));
            Chunk.SetTile(tile, new Point(15, 0, 14));
            Chunk.SetTile(tile, new Point(15, 0, 13));
            Chunk.SetTile(tile, new Point(14, 0, 15));
            Chunk.SetTile(tile, new Point(13, 0, 15));
            Chunk.SetTile(tile, new Point(13, 0, 14));
            Chunk.SetTile(tile, new Point(14, 0, 13));
            Chunk.SetTile(tile, new Point(13, 0, 13));

            Chunk.SetTile(tile, new Point(15, 2, 15));
            Chunk.SetTile(tile, new Point(15, 2, 14));
            Chunk.SetTile(tile, new Point(15, 2, 13));
            Chunk.SetTile(tile, new Point(14, 2, 15));
            Chunk.SetTile(tile, new Point(13, 2, 15));
            Chunk.SetTile(tile, new Point(13, 2, 14));
            Chunk.SetTile(tile, new Point(14, 2, 13));
            Chunk.SetTile(tile, new Point(13, 2, 13));

            Chunk.SetTile(tile, new Point(15, 1, 15));
            Chunk.SetTile(tile, new Point(15, 1, 13));
            Chunk.SetTile(tile, new Point(13, 1, 15));
            Chunk.SetTile(tile, new Point(13, 1, 13));

            Chunk.SetTile(tile, new Point(1, 0, 14));
            Chunk.SetTile(tile, new Point(1, 1, 14));
            Chunk.SetTile(tile, new Point(1, 1, 15));
            Chunk.SetTile(tile, new Point(1, 1, 13));
            Chunk.SetTile(tile, new Point(0, 1, 14));
            Chunk.SetTile(tile, new Point(2, 1, 14));
            Chunk.SetTile(tile, new Point(1, 2, 14));
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
        }
    }
}
