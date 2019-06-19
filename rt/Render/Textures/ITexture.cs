using System;

namespace RT.Render.Textures
{
    interface ITexture : IDisposable
    {
        void Bind(int uniform, int unit = 0);
        int Index { get; }
    }
}
