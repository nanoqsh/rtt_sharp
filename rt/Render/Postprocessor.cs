using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace RT.Render
{
    class Postprocessor : IDisposable
    {
        private readonly ShaderProgram shader;
        private readonly int quadVAO;
        private FrameBuffer? frameBuffer;
        private int width;
        private int height;

        public Postprocessor()
        {
            shader = new ShaderProgram(
                new Shader("post_vs.glsl", ShaderType.VertexShader),
                new Shader("post_fs.glsl", ShaderType.FragmentShader)
                );

            quadVAO = GL.GenVertexArray();
            GL.BindVertexArray(quadVAO);

            int arrayBufferIndex = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, arrayBufferIndex);

            GL.BufferData(
                BufferTarget.ArrayBuffer,
                Marshal.SizeOf(typeof(Vertex4)) * 4,
                Quad.Data,
                BufferUsageHint.StaticDraw
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

            GL.BindVertexArray(0);
        }

        public void Dispose()
        {
            shader.Dispose();

            GL.BindVertexArray(0);
            GL.DeleteVertexArray(quadVAO);

            frameBuffer?.Dispose();
        }

        public void Resize(int width, int height, int pixelSize)
        {
            frameBuffer?.Dispose();
            frameBuffer = new FrameBuffer(width / pixelSize, height / pixelSize);

            this.width = width;
            this.height = height;
        }

        public void Bind()
        {
            GL.Viewport(0, 0, frameBuffer!.Width, frameBuffer!.Heigth);
            frameBuffer!.Bind();
        }

        public void Unbind()
        {
            frameBuffer!.Unbind();
        }

        public void Draw()
        {
            frameBuffer!.Unbind();

            GL.Viewport(0, 0, width, height);

            shader.Enable();

            GL.Uniform1(shader.GetUniformIndex("frame"), 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2DMultisample, frameBuffer!.FrameIndex);

            GL.Uniform1(shader.GetUniformIndex("frame_width"), width);
            GL.Uniform1(shader.GetUniformIndex("frame_height"), height);

            GL.BindVertexArray(quadVAO);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            GL.BindVertexArray(0);

            shader.Disable();
        }
    }
}
