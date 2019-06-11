using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using RT.Engine;

namespace RT.Render
{
    class Frame
    {
        private readonly ShaderProgram shader;
        private readonly Camera camera;
        private Rectangle size;

        public Frame()
        {
            shader = new ShaderProgram(
                new Shader("defVS.glsl", ShaderType.VertexShader),
                new Shader("defFS.glsl", ShaderType.FragmentShader)
                );

            camera = Core.Unit.Player.Camera;

            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
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

            GL.Uniform1(shader.GetUniformIndex("layer0"), 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Core.Unit.SpriteMap.Texture.Index);

            Color4 fogColour = Color4.DeepSkyBlue;
            GL.Uniform4(shader.GetUniformIndex("fog_colour"), fogColour);

            foreach ((Mesh m, Vector3 v) in Core.Unit.Map.World.GetMeshes(shader))
            {
                Matrix4 model = Matrix4.CreateTranslation(v);
                GL.UniformMatrix4(shader.GetUniformIndex("model"), false, ref model);

                m.Draw();
            }

            shader.Disable();
        }

        public void Resize(Rectangle size)
        {
            GL.Viewport(size);
            this.size = size;
        }
    }
}
