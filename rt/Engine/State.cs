using RT.Render;
using System.Collections.Generic;

namespace RT.Engine
{
    class State
    {
        public readonly Model Model;
        public readonly uint[] Layers;
        public readonly Dictionary<string, object> Values;
        public readonly Box Box;

        public State(Model model, uint[] layers, Dictionary<string, object> values, Box box)
        {
            Model = model;
            Layers = layers;
            Values = values;
            Box = box;
        }
    }
}
