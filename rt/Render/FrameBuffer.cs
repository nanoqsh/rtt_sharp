using OpenTK.Graphics.OpenGL4;
using System;

namespace RT.Render
{
    class FrameBuffer : IDisposable
    {
        public readonly int Index;
        public readonly int FrameIndex;
        public readonly int Width;
        public readonly int Heigth;

        private readonly int renderBuffer;

        public FrameBuffer(int width, int height)
        {
            Width = width;
            Heigth = height;

            FrameIndex = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2DMultisample, FrameIndex);

            GL.TexImage2DMultisample(
                TextureTargetMultisample.Texture2DMultisample,
                4,
                PixelInternalFormat.Rgba,
                width,
                height,
                true
                );

            GL.BindTexture(TextureTarget.Texture2DMultisample, 0);

            Index = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.FramebufferExt, Index);

            GL.FramebufferTexture2D(
                FramebufferTarget.FramebufferExt,
                FramebufferAttachment.ColorAttachment0Ext,
                TextureTarget.Texture2DMultisample,
                FrameIndex,
                0
                );

            renderBuffer = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, renderBuffer);
            GL.RenderbufferStorageMultisample(
                RenderbufferTarget.RenderbufferExt,
                4,
                RenderbufferStorage.Depth24Stencil8,
                width,
                height
                );

            GL.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, 0);
            GL.FramebufferRenderbuffer(
                FramebufferTarget.FramebufferExt,
                FramebufferAttachment.DepthAttachmentExt,
                RenderbufferTarget.RenderbufferExt,
                renderBuffer
                );

            FramebufferErrorCode status = GL.CheckFramebufferStatus(FramebufferTarget.FramebufferExt);
            if (status != FramebufferErrorCode.FramebufferComplete)
                throw new Exception($"Error: {status}");

            GL.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
        }

        public void Dispose()
        {
            GL.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
            GL.DeleteRenderbuffer(renderBuffer);
            GL.DeleteFramebuffer(Index);
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.FramebufferExt, Index);
        }

        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
        }
    }
}
