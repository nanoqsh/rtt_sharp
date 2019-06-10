using OpenTK;
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

        public static readonly State Empty = new State(
            new Model(new Face[0], Side.None),
            new uint[0],
            new Box()
            );
    }
}
