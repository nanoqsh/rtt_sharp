using System.Drawing;

namespace RT.Render
{
    class SpriteMap
    {
        private readonly Texture Texture;
        private readonly int size;

        public SpriteMap(Bitmap bitmap, int size)
        {
            Texture = new Texture(bitmap);
            this.size = size;
        }

        public OpenTK.Vector2[] GetUV(uint spriteID, OpenTK.Vector2[] originals)
        {
            OpenTK.Vector2[] result = new OpenTK.Vector2[originals.Length];

            float x = (((int)spriteID) % size) / (float)size;
            float y = (((int)spriteID) / size) / (float)size;

            for (int i = 0; i < originals.Length; ++i)
                result[i] = new OpenTK.Vector2(
                    originals[i].X / size + x,
                    originals[i].Y / size + y
                    );

            return result;
        }
    }
}
