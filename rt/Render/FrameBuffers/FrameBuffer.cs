using OpenTK.Graphics.OpenGL4;
using System;

namespace RT.Render.FrameBuffers
{
    class FrameBuffer : IFrameBuffer
    {
        private readonly int frame;
        private readonly int renderBuffer;
        private readonly int frameBuffer;

        public FrameBuffer(int width, int height)
        {
            frame = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, frame);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                width,
                height,
                0,
                PixelFormat.Bgra,
                PixelType.UnsignedByte,
                IntPtr.Zero
                );


            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.ClampToEdge
                );

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapT,
                (int)TextureWrapMode.ClampToEdge
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

            GL.BindTexture(TextureTarget.Texture2D, 0);

            frameBuffer = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBuffer);

            GL.FramebufferTexture2D(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.ColorAttachment0,
                TextureTarget.Texture2D,
                frame,
                0
                );

            renderBuffer = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBuffer);

            GL.RenderbufferStorage(
                RenderbufferTarget.Renderbuffer,
                RenderbufferStorage.Depth24Stencil8,
                width,
                height
                );

            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            GL.FramebufferRenderbuffer(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.DepthStencilAttachment,
                RenderbufferTarget.Renderbuffer,
                renderBuffer
                );

            FramebufferErrorCode status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (status != FramebufferErrorCode.FramebufferComplete)
                throw new Exception($"Error: {status}");

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBuffer);
        }

        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void BindFrameTexture(int frameUniformIndex)
        {
            GL.Uniform1(frameUniformIndex, 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, frame);
        }

        public void Dispose()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.DeleteRenderbuffer(renderBuffer);
            GL.DeleteFramebuffer(frameBuffer);
        }
    }
}
