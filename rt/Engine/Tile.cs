using System;

namespace RT.Engine
{
    class Tile
    {
        public readonly State[] States;
        public readonly uint ID;

        public Tile(State[] states, uint id)
        {
            if (states.Length == 0)
                throw new Exception("Tile must have at least one state!");

            States = states;
            ID = id;
        }

        public virtual State DetectState(Chunk chunk) => DefaultState;

        public State DefaultState => States[0];

        public static readonly Tile Empty = new Tile(new State[] { State.Empty }, 0);
    }
}
