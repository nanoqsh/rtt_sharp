using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using RT.Render.FrameBuffers;

namespace RT.Render
{
    class Postprocessor : IDisposable
    {
        private readonly ShaderProgram shader;
        private readonly int quadVAO;
        private IFrameBuffer? frameBuffer;
        private int width;
        private int height;
        private int pixelSize;

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

            int samples = 0;

            if (samples == 0)
                frameBuffer = new FrameBuffer(
                    width / pixelSize,
                    height / pixelSize
                    );
            else
                frameBuffer = new FrameBufferMultisample(
                    width / pixelSize,
                    height / pixelSize,
                    samples
                    );

            this.width = width;
            this.height = height;
            this.pixelSize = pixelSize;
        }

        public void Bind()
        {
            GL.Viewport(0, 0, width / pixelSize, height / pixelSize);
            frameBuffer!.Bind();
        }

        public void Draw()
        {
            frameBuffer!.Unbind();

            GL.Viewport(0, 0, width, height);

            shader.Enable();

            frameBuffer!.BindFrameTexture(shader.GetUniformIndex("frame"));

            // samples != 0
            // GL.Uniform1(shader.GetUniformIndex("frame_width"), width / pixelSize);
            // GL.Uniform1(shader.GetUniformIndex("frame_height"), height / pixelSize);

            GL.BindVertexArray(quadVAO);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            GL.BindVertexArray(0);

            shader.Disable();
        }
    }
}
