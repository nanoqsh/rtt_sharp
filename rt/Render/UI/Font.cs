using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using RT.Render.Textures;
using System;
using System.Runtime.InteropServices;

namespace RT.Render.UI
{
    class Font
    {
        public readonly int CharWidth;
        public readonly int CharHeight;
        private readonly Frame frame;
        private readonly FontTexture texture;
        private readonly int mapWidth;
        private readonly int mapHeight;
        private readonly ShaderProgram shader;
        private readonly int vao;
        private readonly int vbo;
        public int Scale = 1;
        public bool Inverted = false;

        public Font(Frame frame)
        {
            CharWidth = 8;
            CharHeight = 8;
            this.frame = frame;

            mapWidth = 16;
            mapHeight = 16;

            texture = new FontTexture(Loader.LoadFontImage("0.png"));

            shader = new ShaderProgram(
                new Shader("font_vs.glsl", ShaderType.VertexShader),
                new Shader("font_fs.glsl", ShaderType.FragmentShader)
                );

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            GL.BufferData(
                BufferTarget.ArrayBuffer,
                Marshal.SizeOf(typeof(Vertex4)) * 4,
                IntPtr.Zero,
                BufferUsageHint.DynamicDraw
                );

            Attribute attr = Vertex4.GetAttribute(shader, "vertex");

            GL.EnableVertexAttribArray(attr.Index);
            GL.VertexAttribPointer(
                attr.Index,
                attr.SizeOfElements,
                VertexAttribPointerType.Float,
                false,
                attr.StrideOfElements * sizeof(float),
                attr.OffsetOfElements * sizeof(float)
                );

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public void Draw(string text, int x, int y) =>
            Draw(text, x, y, Color4.White);

        public void Draw(string text, int x, int y, Color4 color)
        {
            shader.Enable();

            Matrix4 projection = frame.Ortho;
            GL.UniformMatrix4(shader.GetUniformIndex("projection"), false, ref projection);

            texture.Bind(shader.GetUniformIndex("font"));

            int inverted = Inverted ? 1 : 0;
            GL.Uniform1(shader.GetUniformIndex("inverted"), inverted);

            GL.BindVertexArray(vao);

            Color4 shadow = new Color4(0, 0, 0, 100);
            GL.Uniform4(shader.GetUniformIndex("font_color"), shadow);
            DrawText(text, x + Scale, y - Scale);

            GL.Uniform4(shader.GetUniformIndex("font_color"), color);
            DrawText(text, x, y);

            shader.Disable();
        }

        private void DrawText(string text, int x, int y)
        {
            for (int i = 0; i < text.Length; ++i)
            {
                int n = text[i];

                float sizeU = 1.0f / mapWidth;
                float sizeV = 1.0f / mapHeight;

                float u = (float)(n % mapWidth) / mapWidth;
                float v = (float)(n / mapHeight) / mapHeight;

                int width = CharWidth * Scale;
                int height = CharHeight * Scale;

                Vertex4[] vertices = new Vertex4[]
                {
                    new Vertex4(x + i * width, y, u, v + sizeV),
                    new Vertex4(x + i * width, y + height, u, v),
                    new Vertex4(x + i * width + width, y, u + sizeU, v + sizeV),
                    new Vertex4(x + i * width + width, y + height, u + sizeU, v)
                };

                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, Marshal.SizeOf(typeof(Vertex4)) * 4, vertices);

                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            }
        }
    }
}
