using OpenTK;
using RT.Engine;

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
