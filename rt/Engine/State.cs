using RT.Render;
using System.Collections.Generic;

namespace RT.Engine
{
    class State
    {
        public readonly Model Model;
        public readonly Dictionary<string, uint> Layers;
        public readonly Dictionary<string, object> Values;
    }
}
