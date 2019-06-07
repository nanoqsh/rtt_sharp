using OpenTK;

namespace RT.Engine
{
    class Map
    {
        private readonly Player player;

        public Map(Player player)
        {
            this.player = player;
        }

        public void Update(float delta, Controller controller)
        {
            float speed = 0.02f;
            Vector3 move = new Vector3();

            if (controller.IsKeyPressed(controller.MoveForward))
                move += player.Camera.Front * speed;

            if (controller.IsKeyPressed(controller.MoveBack))
                move -= player.Camera.Front * speed;

            if (controller.IsKeyPressed(controller.MoveLeft))
                move -=
                    Vector3.Normalize(Vector3.Cross(player.Camera.Front, Vector3.UnitY))
                    * speed;

            if (controller.IsKeyPressed(controller.MoveRight))
                move += 
                    Vector3.Normalize(Vector3.Cross(player.Camera.Front, Vector3.UnitY))
                    * speed;

            if (controller.IsKeyPressed(controller.MoveUp))
                move += Vector3.UnitY * speed;

            if (controller.IsKeyPressed(controller.MoveDown))
                move -= Vector3.UnitY * speed;

            player.Move(move);
            player.Camera.Rotate(controller.MouseDelta);
        }
    }
}
