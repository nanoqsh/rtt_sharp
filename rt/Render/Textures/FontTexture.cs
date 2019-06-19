using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;

namespace RT.Render.Textures
{
    class FontTexture : ITexture
    {
        public int Index { get; private set; }

        public FontTexture(Bitmap bitmap)
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
                OpenTK.Graphics.OpenGL4.PixelFormat.Rgba,
                PixelType.UnsignedByte,
                bitData.Scan0
                );

            bitmap.UnlockBits(bitData);

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
        }

        public void Dispose()
        {
            GL.DeleteTexture(Index);
        }

        public void Bind(int uniform, int unit = 0)
        {
            GL.Uniform1(uniform, 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Index);
        }
    }
}
