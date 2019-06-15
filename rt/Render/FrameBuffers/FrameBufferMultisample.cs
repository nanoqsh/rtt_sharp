using OpenTK.Graphics.OpenGL4;
using System;

namespace RT.Render.FrameBuffers
{
    class FrameBufferMultisample : IFrameBuffer
    {
        private readonly int frame;
        private readonly int renderBuffer;
        private readonly int frameBuffer;

        public FrameBufferMultisample(int width, int height, int samples)
        {
            frame = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2DMultisample, frame);

            GL.TexImage2DMultisample(
                TextureTargetMultisample.Texture2DMultisample,
                samples,
                PixelInternalFormat.Rgba,
                width,
                height,
                true
                );

            GL.BindTexture(TextureTarget.Texture2DMultisample, 0);

            frameBuffer = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBuffer);

            GL.FramebufferTexture2D(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.ColorAttachment0,
                TextureTarget.Texture2DMultisample,
                frame,
                0
                );

            renderBuffer = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBuffer);

            GL.RenderbufferStorageMultisample(
                RenderbufferTarget.Renderbuffer,
                4,
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
            GL.BindTexture(TextureTarget.Texture2DMultisample, frame);
        }

        public void Dispose()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.DeleteRenderbuffer(renderBuffer);
            GL.DeleteFramebuffer(frameBuffer);
        }
    }
}
