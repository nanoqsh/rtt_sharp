using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace RT.Render.Textures
{
    class Texture : ITexture
    {
        public int Index { get; private set; }

        public Texture(Bitmap bitmap)
        {
            Index = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, Index);

            BitmapData bitData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb
                );

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                bitData.Width,
                bitData.Height,
                0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
                PixelType.UnsignedByte,
                bitData.Scan0
                );

            bitmap.UnlockBits(bitData);

            SetParameters();

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public Texture(int width, int height)
        {
            Index = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, Index);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                width,
                height,
                0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
                PixelType.UnsignedByte,
                IntPtr.Zero
                );

            SetParameters();

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            GL.DeleteTexture(Index);
        }

        protected virtual void SetParameters()
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

        public void Bind(int uniform, int unit = 0)
        {
            GL.Uniform1(uniform, unit);
            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            GL.BindTexture(TextureTarget.Texture2D, Index);
        }
    }
}
