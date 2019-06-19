using OpenTK.Graphics.OpenGL4;
using RT.Render.Textures;
using System;

namespace RT.Render.FrameBuffers
{
    class FrameBufferMultisample : IFrameBuffer
    {
        private readonly TextureMultisample frame;
        private readonly int renderBuffer;
        private readonly int frameBuffer;

        private readonly Texture finalFrame;
        private readonly int finalRenderBuffer;
        private readonly int finalFrameBuffer;

        private readonly int width;
        private readonly int height;

        public FrameBufferMultisample(int width, int height, int samples)
        {
            this.width = width;
            this.height = height;

            frame = new TextureMultisample(width, height, samples);

            frameBuffer = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBuffer);

            GL.FramebufferTexture2D(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.ColorAttachment0,
                TextureTarget.Texture2DMultisample,
                frame.Index,
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

            CheckError();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            finalFrame = new Texture(width, height);

            finalFrameBuffer = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, finalFrameBuffer);

            GL.FramebufferTexture2D(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.ColorAttachment0,
                TextureTarget.Texture2D,
                finalFrame.Index,
                0
                );

            finalRenderBuffer = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, finalRenderBuffer);

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
                finalRenderBuffer
                );

            CheckError();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        private void CheckError()
        {
            FramebufferErrorCode status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (status != FramebufferErrorCode.FramebufferComplete)
                throw new Exception($"Error: {status}");
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBuffer);
        }

        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, frameBuffer);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, finalFrameBuffer);
            GL.BlitFramebuffer(
                0,
                0,
                width,
                height,
                0,
                0,
                width,
                height,
                ClearBufferMask.ColorBufferBit,
                BlitFramebufferFilter.Nearest
                );

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void BindFrameTexture(int frameUniformIndex) =>
            finalFrame.Bind(frameUniformIndex);

        public void Dispose()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            frame.Dispose();
            finalFrame.Dispose();

            GL.DeleteRenderbuffer(renderBuffer);
            GL.DeleteFramebuffer(frameBuffer);
            GL.DeleteRenderbuffer(finalRenderBuffer);
            GL.DeleteFramebuffer(finalFrameBuffer);
        }
    }
}
