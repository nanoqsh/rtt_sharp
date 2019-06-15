using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using RT.Engine;
using RT.Render.UI;

namespace RT.Render
{
    class Frame
    {
        private readonly ShaderProgram shader;
        private readonly Camera camera;
        public Rectangle Size { get; private set; }
        public Matrix4 Ortho { get; private set; }
        private readonly Font font;
        private readonly Postprocessor post;
        private readonly int pixelSize = 1;

        public Frame()
        {
            shader = new ShaderProgram(
                new Shader("def_vs.glsl", ShaderType.VertexShader),
                new Shader("def_fs.glsl", ShaderType.FragmentShader)
                );

            camera = Core.Unit.Player.Camera;

            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);

            GL.Enable(EnableCap.Multisample);

            font = new Font(this);
            post = new Postprocessor();
        }

        public void Draw()
        {
            post.Bind();

            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(Color.DarkCyan);
            GL.Clear(
                  ClearBufferMask.ColorBufferBit
                | ClearBufferMask.DepthBufferBit
                );

            shader.Enable();

            Matrix4 projection = camera.Projection(Size);
            GL.UniformMatrix4(shader.GetUniformIndex("projection"), false, ref projection);

            Matrix4 view = camera.View;
            GL.UniformMatrix4(shader.GetUniformIndex("view"), false, ref view);

            GL.Uniform1(shader.GetUniformIndex("layer0"), 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Core.Unit.SpriteMap.Texture.Index);

            Color4 fogColour = Color4.DarkCyan;
            GL.Uniform4(shader.GetUniformIndex("fog_colour"), fogColour);

            foreach ((Mesh m, Vector3 v) in Core.Unit.Map.World.GetMeshes(shader))
            {
                Matrix4 model = Matrix4.CreateTranslation(v);
                GL.UniformMatrix4(shader.GetUniformIndex("model"), false, ref model);

                m.Draw();
            }

            shader.Disable();

            GL.Disable(EnableCap.DepthTest);

            post.Draw();

            /*
            int step = 32;
            font.Scale = 4;
            
            font.Inverted = true;
            font.Draw(" `1234567890-=qwertyuiop[]", -400, 0);
            font.Inverted = false;
            font.Draw("asdfghjkl;'\\zxcvbnm,./", -400, -step - font.Scale);
            font.Inverted = true;
            font.Draw("~!@#$%^&*()_+QWERTYUIOP{}", -400, -step * 2 - font.Scale * 2);
            font.Inverted = false;
            font.Draw("ASDFGHJKL:\"|ZXCVBNM<>?", -400, -step * 3 - font.Scale * 3);
            */
        }

        public void Resize(Rectangle size)
        {
            GL.Viewport(size);
            Size = size;
            Ortho = Matrix4.CreateOrthographic(size.Width, size.Height, -100, 100);

            post.Resize(size.Width, size.Height, pixelSize);
        }
    }
}
