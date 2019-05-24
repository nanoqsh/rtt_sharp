using OpenTK;
using RT.Engine;
using System;

namespace RT.Render
{
    class Scene
    {
        private readonly Camera camera;

        public Scene()
        {
            camera = new Camera(new Vector3(-2, 1, 0));
        }

        public void Draw()
        {
        }
    }
}
