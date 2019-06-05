using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace RT.Render
{
    class Frame
    {
        private readonly ShaderProgram shader;

        public Frame()
        {
            shader = new ShaderProgram(
                new Shader("defVS.glsl", ShaderType.VertexShader),
                new Shader("defFS.glsl", ShaderType.FragmentShader)
                );
        }

        public void Draw()
        {
            GL.ClearColor(Color.DeepSkyBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void Resize(Rectangle size)
        {
            GL.Viewport(size);
        }
    }
}
