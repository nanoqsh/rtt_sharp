using OpenTK;
using OpenTK.Input;
using System.Collections.Generic;

namespace RT.Engine
{
    class Controller
    {
        public Key MoveForward = Key.W;
        public Key MoveBack = Key.S;
        public Key MoveLeft = Key.A;
        public Key MoveRight = Key.D;
        public Key MoveUp = Key.Space;
        public Key MoveDown = Key.ShiftLeft;

        public float MouseSensitivity = 0.5f;

        private Vector2 lastPosition;
        public Vector2 MouseDelta { get; private set; }

        private readonly HashSet<Key> pressedKeys;

        public Controller()
        {
            pressedKeys = new HashSet<Key>();
        }

        public void DownKey(Key key) => pressedKeys.Add(key);
        public void UpKey(Key key) => pressedKeys.Remove(key);
        public bool IsKeyPressed(Key key) => pressedKeys.Contains(key);

        public Vector2 MouseUpdate()
        {
            Vector2 mouse = new Vector2(
                Mouse.GetState().X * MouseSensitivity,
                Mouse.GetState().Y * MouseSensitivity
                );

            MouseDelta = lastPosition - mouse;
            lastPosition = mouse;

            return MouseDelta;
        }
    }
}
