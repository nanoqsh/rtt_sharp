using OpenTK;
using RT.Render.Textures;

namespace RT.Render
{
    class SpriteMap
    {
        public readonly Texture Texture;
        private readonly int size;

        public SpriteMap(System.Drawing.Bitmap bitmap, int size)
        {
            Texture = new Texture(bitmap);
            this.size = size;
        }

        public Vector2[] GetUV(uint spriteID, Vector2[] originals)
        {
            Vector2[] result = new Vector2[originals.Length];

            float x = ((int)spriteID) % size / (float)size;
            float y = ((int)spriteID) / size / (float)size;

            for (int i = 0; i < originals.Length; ++i)
                result[i] = new Vector2(
                    originals[i].X / size + x,
                    originals[i].Y / size + y
                    );

            return result;
        }
    }
}
