using RT.Render;

namespace RT.Engine
{
    class State
    {
        public readonly Model Model;
        public readonly uint[] Layers;
        public readonly Box Box;

        public State(Model model, uint[] layers, Box box)
        {
            Model = model;
            Layers = layers;
            Box = box;
        }
    }
}
