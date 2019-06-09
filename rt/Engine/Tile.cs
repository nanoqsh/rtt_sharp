using System;

namespace RT.Engine
{
    class Tile
    {
        public readonly State[] States;

        public Tile(State[] states)
        {
            if (states.Length == 0)
                throw new Exception("Tile must have at least one state!");

            States = states;
        }

        public State DefaultState => States[0];

        public static readonly Tile Empty = new Tile(new State[]
            {
                State.Empty
            });
    }
}
