namespace RT.Engine
{
    struct Block
    {
        public Tile Tile;
        public State State;

        public Block(Tile tile, State state)
        {
            Tile = tile;
            State = state;
        }

        public static readonly Block Empty = new Block(Tile.Empty, State.Empty);
        public bool IsEmpty => Tile == Tile.Empty;
    }
}
