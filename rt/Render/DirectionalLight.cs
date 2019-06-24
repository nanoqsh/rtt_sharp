using OpenTK;

namespace RT.Render
{
    class DirectionalLight
    {
        private Matrix4? projection = null;
        public Matrix4 Projection
        {
            get
            {
                if (projection != null)
                    return (Matrix4)projection;

                float zNear = 1.0f;
                float zFar = 8.0f;

                return Matrix4.CreateOrthographic(20.0f, 20.0f, zNear, zFar);
            }
        }

        private Matrix4? view;
        public Matrix4 View =>
            view ?? (Matrix4)(view = Matrix4.LookAt(
                new Vector3(-2.0f, 4.0f, -1.0f),
                Vector3.Zero,
                Vector3.UnitY
                ));
    }
}
