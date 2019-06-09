using OpenTK;
using OpenTK.Graphics.OpenGL4;
using RT.Engine;

namespace RT.Render
{
    class Frame
    {
        private readonly ShaderProgram shader;
        private readonly Camera camera;
        private Rectangle size;
        private readonly Mesh mesh;

        public Frame()
        {
            shader = new ShaderProgram(
                new Shader("defVS.glsl", ShaderType.VertexShader),
                new Shader("defFS.glsl", ShaderType.FragmentShader)
                );

            camera = Core.Unit.Player.Camera;

            Tile brick = Core.Unit.Resource.LoadTile("debug.json");
            mesh = new Mesh(brick.States[0], shader);

            GL.Enable(EnableCap.DepthTest);
        }

        public void Draw()
        {
            GL.ClearColor(Color.DeepSkyBlue);
            GL.Clear(
                  ClearBufferMask.ColorBufferBit
                | ClearBufferMask.DepthBufferBit
                );

            shader.Enable();

            Matrix4 projection = camera.Projection(size);
            GL.UniformMatrix4(shader.GetUniformIndex("projection"), false, ref projection);

            Matrix4 view = camera.View;
            GL.UniformMatrix4(shader.GetUniformIndex("view"), false, ref view);

            Matrix4 model = Matrix4.Identity;
            GL.UniformMatrix4(shader.GetUniformIndex("model"), false, ref model);

            GL.Uniform1(shader.GetUniformIndex("layer0"), 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Core.Unit.SpriteMap.Texture.Index);

            mesh.Draw();
            shader.Disable();
        }

        public void Resize(Rectangle size)
        {
            GL.Viewport(size);
            this.size = size;
        }
    }
}
