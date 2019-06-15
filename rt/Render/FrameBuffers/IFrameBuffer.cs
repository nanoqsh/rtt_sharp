using System;

namespace RT.Render.FrameBuffers
{
    interface IFrameBuffer : IDisposable
    {
        void Bind();
        void Unbind();
        void BindFrameTexture(int frameUniformIndex);
    }
}
