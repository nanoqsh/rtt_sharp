using Newtonsoft.Json;
using System.Linq;

namespace RT.Scheme
{
    class Tile
    {
        public readonly string[]? Models;
        public readonly string[]? Textures;
        public readonly string? Cover;
        public readonly string? CoverSides;
        public readonly State? Default;
        public readonly State[]? States;
        public readonly State[]? AddStates;
        public readonly string? Parent;

        public Tile(string[]? models, string[]? textures, string? cover, string? coverSides, [JsonProperty("default")] State? defaultState, State[]? states, State[]? addStates, string? parent)
        {
            Models = models;
            Textures = textures;
            Cover = cover;
            CoverSides = coverSides;
            Default = defaultState;
            States = states;
            AddStates = addStates;
            Parent = parent;
        }

        public static Tile Inherit(Tile child, Tile parent) =>
            new Tile(
                child.Models ?? parent.Models,
                child.Textures ?? parent.Textures,
                child.Cover ?? parent.Cover,
                child.CoverSides ?? parent.CoverSides,
                State.Inherit(
                    child.Default ?? State.Empty,
                    parent.Default ?? State.Empty
                    ),
                child.AddStates == null
                    ? child.States ?? parent.States
                    : parent.States != null
                        ? parent.States.Concat(child.AddStates).ToArray()
                        : child.AddStates,
                null,
                null
                );
    }
}
