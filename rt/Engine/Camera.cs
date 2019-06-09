using System;
using OpenTK;

namespace RT.Engine
{
    class Camera
    {
        private const float MAX_PITCH = 89.5f;
        private const float MIN_PITCH = -89.5f;

        private Vector2 rotation;
        public Vector3 Position { get; private set; }
        public Vector3 Front { get; private set; }

        public Camera(Vector3 position)
            : this(position, new Vector3(1, 0, 0), Vector2.Zero)
        { }

        public Camera(Vector3 position, Vector3 front)
            : this(position, front, Vector2.Zero)
        { }

        public Camera(Vector3 position, Vector3 front, Vector2 rotation)
        {
            Position = position;
            Front = Vector3.Normalize(front);
            this.rotation = rotation;
        }

        private Matrix4? view;
        public Matrix4 View =>
            view ?? (Matrix4)(view = Matrix4.LookAt(
                Position,
                Position + Front,
                Vector3.UnitY
                ));

        private bool perspective = true;
        public bool Perspective
        {
            get => perspective;
            set
            {
                projection = null;
                perspective = value;
            }
        }

        private Matrix4? projection = null;
        private Rectangle? oldSize = null;
        public Matrix4 Projection(Rectangle size)
        {
            if (projection != null && oldSize == size)
                return (Matrix4)projection;

            oldSize = size;
            return (Matrix4)(projection = perspective
                ? Matrix4.CreatePerspectiveFieldOfView(
                    1.5f,
                    size.Width / (float)size.Height,
                    0.1f,
                    100.0f
                    )
                : Matrix4.CreateOrthographic(
                    2f * size.Width / size.Height,
                    2f,
                    0.1f,
                    100.0f
                    ));
        }

        public void Move(Vector3 delta)
        {
            Position += delta;
            view = null;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
            view = null;
        }

        private static double Convert(double x) => Math.PI / 180 * x;

        public void Rotate(Vector2 delta)
        {
            rotation.X = (rotation.X + delta.X) % 360;
            rotation.Y += delta.Y;

            if (rotation.Y > MAX_PITCH)
                rotation.Y = MAX_PITCH;

            if (rotation.Y < MIN_PITCH)
                rotation.Y = MIN_PITCH;

            Front = Vector3.Normalize(new Vector3(
                (float)Math.Sin(Convert(rotation.X)) * (float)Math.Cos(Convert(rotation.Y)),
                (float)Math.Sin(Convert(rotation.Y)),
                (float)Math.Cos(Convert(rotation.X)) * (float)Math.Cos(Convert(rotation.Y))
                ));

            view = null;
        }
    }
}
