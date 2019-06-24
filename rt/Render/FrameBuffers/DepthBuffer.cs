using OpenTK.Graphics.OpenGL4;
using System;

namespace RT.Render.FrameBuffers
{
    class DepthBuffer : IFrameBuffer
    {
        public const int SHADOW_WIDTH = 1024;
        public const int SHADOW_HEIGHT = 1024;

        private readonly int depthFrameBuffer;
        private readonly int depthFrame;

        public DepthBuffer()
        {
            depthFrameBuffer = GL.GenFramebuffer();

            depthFrame = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, depthFrame);
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.DepthComponent,
                SHADOW_WIDTH,
                SHADOW_HEIGHT,
                0,
                PixelFormat.DepthComponent,
                PixelType.Float,
                IntPtr.Zero
                );

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.Repeat
                );

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapT,
                (int)TextureWrapMode.Repeat
                );

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Nearest
                );

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Nearest
                );

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, depthFrameBuffer);
            GL.FramebufferTexture2D(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.DepthAttachment,
                TextureTarget.Texture2D,
                depthFrame,
                0
                );

            GL.DrawBuffer(DrawBufferMode.None);
            GL.ReadBuffer(ReadBufferMode.None);

            FramebufferErrorCode status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (status != FramebufferErrorCode.FramebufferComplete)
                throw new Exception($"Error: {status}");

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, depthFrameBuffer);
        }

        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void BindFrameTexture(int frameUniformIndex)
        {
            GL.Uniform1(frameUniformIndex, 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, depthFrame);
        }

        public void Dispose()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.DeleteFramebuffer(depthFrameBuffer);
        }
    }
}
