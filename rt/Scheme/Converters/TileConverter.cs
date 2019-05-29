using System.Linq;

namespace RT.Scheme.Converters
{
    static class TileConverter
    {
        public static Engine.Tile Convert(Tile tile)
        {
            Engine.State ConvertState(State state) =>
                StateConverter.Convert(tile.Default == null
                    ? state
                    : State.Inherit(state, tile.Default), tile);

            Engine.State[] states = tile.States == null
                ? new Engine.State[0]
                : tile.States.Select(ConvertState).ToArray();

            return new Engine.Tile(states);
        }
    }
}
