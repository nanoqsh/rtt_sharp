using OpenTK;

namespace RT.Engine
{
    class Player
    {
        public readonly string Name;
        public Vector3 Position { get; private set; }
        public readonly Camera Camera;

        public Player(string name, Vector3 position)
        {
            Name = name;
            Position = position;
            Camera = new Camera(position);
        }

        public void Move(Vector3 delta)
        {
            Position += delta;
            Camera.Position = Position;
        }
    }
}
