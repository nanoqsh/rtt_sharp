using OpenTK.Graphics.OpenGL4;

namespace RT.Render.Textures
{
    class TextureMultisample : ITexture
    {
        public int Index { get; private set; }

        public TextureMultisample(int width, int height, int samples)
        {
            Index = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2DMultisample, Index);

            GL.TexImage2DMultisample(
                TextureTargetMultisample.Texture2DMultisample,
                samples,
                PixelInternalFormat.Rgba,
                width,
                height,
                true
                );

            GL.BindTexture(TextureTarget.Texture2DMultisample, 0);
        }

        public void Bind(int uniform, int unit = 0)
        {
            GL.Uniform1(uniform, unit);
            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            GL.BindTexture(TextureTarget.Texture2DMultisample, Index);
        }

        public void Dispose()
        {
            GL.DeleteTexture(Index);
        }
    }
}
