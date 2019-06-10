using OpenTK.Graphics.OpenGL4;
using RT.Engine;
using RT.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace RT.Render
{
    class Atlas
    {
        private readonly Dictionary<string, uint> ids;
        private readonly List<string> sprites;
        private readonly int spriteSize;

        public Atlas(int spriteSize)
        {
            ids = new Dictionary<string, uint>();
            sprites = new List<string>();
            this.spriteSize = spriteSize;
        }

        public uint LoadSprite(string file)
        {
            if (ids.TryGetValue(file, out uint value))
                return value;

            uint id = (uint)sprites.Count;
            ids.Add(file, id);
            sprites.Add(file);
            return id;
        }

        public SpriteMap GlueSprites()
        {
            List<Bitmap> images = new List<Bitmap>();

            foreach (string spriteName in sprites)
            {
                Bitmap sprite = Loader.LoadImage(spriteName);

                if (Math.Max(sprite.Width, sprite.Height) != spriteSize)
                    throw new SpriteGlueException(spriteName, spriteSize, Math.Max(sprite.Width, sprite.Height));

                if (sprite.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                    sprite = sprite.ConvertTo32bpp();

                images.Add(sprite);
            }

            int sizeInSprites = (int)Math.Ceiling(Math.Sqrt(images.Count));
            int textureSize = sizeInSprites * spriteSize;

            if (textureSize > GL.GetInteger(GetPName.MaxTextureSize))
                throw new Exception($"Can't make a texture with {images.Count} images ({spriteSize} size)!");

            Bitmap map = new Bitmap(textureSize, textureSize);

            for (int i = 0; i < images.Count; ++i)
                map.CopyArea(
                    images[i],
                    (i % sizeInSprites) * spriteSize,
                    (sizeInSprites - (i / sizeInSprites) - 1) * spriteSize,
                    spriteSize
                    );

            SpriteMap spriteMap = new SpriteMap(map, sizeInSprites);
            UpdateTextureMap(spriteMap);
            return spriteMap;
        }

        private void UpdateTextureMap(SpriteMap spriteMap)
        {
            foreach (Tile tile in Core.Unit.Resource.LoadedTiles)
                foreach (State state in tile.States)
                    foreach (Face face in state.Model.Faces)
                        face.UpdateTextureMap(spriteMap.GetUV(
                            state.Layers[face.Layer],
                            face.TextureMap
                            ));
        }
    }
}
