using System;
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

            if (tile.Default == null && tile.States == null)
                throw new Exception("No any state found!");

            Engine.State[] states = tile.States == null
                ? new Engine.State[0]
                : tile.States.Select(ConvertState).ToArray();

            return new Engine.Tile(states);
        }
    }
}
