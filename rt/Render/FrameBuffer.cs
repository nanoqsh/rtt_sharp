using OpenTK.Graphics.OpenGL4;
using System;

namespace RT.Render
{
    class FrameBuffer : IDisposable
    {
        public readonly int Index;
        public readonly int Width;
        public readonly int Heigth;

        private readonly int renderBuffer;
        private readonly int frameIndex;
        private readonly TextureTarget textureTarget;

        public FrameBuffer(int width, int height, int samples = 0)
        {
            Width = width;
            Heigth = height;

            textureTarget = samples == 0
                ? TextureTarget.Texture2D
                : TextureTarget.Texture2DMultisample;

            frameIndex = GL.GenTexture();
            GL.BindTexture(textureTarget, frameIndex);

            if (samples == 0)
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
            else
                GL.TexImage2DMultisample(
                    TextureTargetMultisample.Texture2DMultisample,
                    samples,
                    PixelInternalFormat.Rgba,
                    width,
                    height,
                    true
                    );

            if (samples == 0)
            {
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
            }

            GL.BindTexture(textureTarget, 0);

            Index = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Index);

            GL.FramebufferTexture2D(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.ColorAttachment0,
                textureTarget,
                frameIndex,
                0
                );

            renderBuffer = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBuffer);

            if (samples == 0)
                GL.RenderbufferStorage(
                    RenderbufferTarget.Renderbuffer,
                    RenderbufferStorage.Depth24Stencil8,
                    width,
                    height
                    );
            else
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

        public void Dispose()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.DeleteRenderbuffer(renderBuffer);
            GL.DeleteFramebuffer(Index);
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Index);
        }

        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void BindFrameTexture(int frameUniformIndex)
        {
            GL.Uniform1(frameUniformIndex, 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(textureTarget, frameIndex);
        }
    }
}
