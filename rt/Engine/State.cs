using RT.Render;

namespace RT.Engine
{
    class State
    {
        public readonly Model Model;
        public readonly uint ID;
        public readonly uint[] Layers;
        public readonly Box Box;

        public State(Model model, uint id, uint[] layers, Box box)
        {
            Model = model;
            ID = id;
            Layers = layers;
            Box = box;
        }

        public static readonly State Empty = new State(
            new Model(new Face[0], Side.None),
            0,
            new uint[0],
            new Box()
            );

        public bool Contain(Side side) => Model.FullSides.Contain(side);
    }
}
