using OpenTK.Graphics.OpenGL4;
using System.Drawing;

namespace RT.Render.Textures
{
    class TextureAniso : Texture
    {
        public TextureAniso(Bitmap bitmap) : base(bitmap)
        {
        }

        public TextureAniso(int width, int height) : base(width, height)
        {
        }

        protected override void SetParameters()
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
                (int)TextureMinFilter.NearestMipmapNearest
                );

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Nearest
                );

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            float maxAniso = GL.GetFloat((GetPName)All.MaxTextureMaxAnisotropy);
            GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)All.MaxTextureMaxAnisotropy, maxAniso);
        }
    }
}
