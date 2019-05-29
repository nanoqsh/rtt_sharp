using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace RT.Scheme
{
    class Tile
    {
        public readonly string[]? Models;
        public readonly string[]? Textures;
        public readonly string? Cover;
        public readonly string? CoverSides;
        public readonly Dictionary<string, string>? Properties;
        public readonly State? Default;
        public readonly State[]? States;
        public readonly string? Parent;

        public Tile(string[]? models, string[]? textures, string? cover, string? coverSides, Dictionary<string, string>? properties, [JsonProperty("default")] State? defaultState, State[]? states, string? parent)
        {
            Models = models;
            Textures = textures;
            Cover = cover;
            CoverSides = coverSides;
            Properties = properties;
            Default = defaultState;
            States = states;
            Parent = parent;
        }

        public static Tile Inherit(Tile child, Tile parent)
        {
            State defaultState = State.Inherit(
                child.Default ?? State.Empty,
                parent.Default ?? State.Empty
                );

            return new Tile(
                child.Models ?? parent.Models,
                child.Textures ?? parent.Textures,
                child.Cover ?? parent.Cover,
                child.CoverSides ?? parent.CoverSides,
                child.Properties ?? parent.Properties,
                defaultState,
                child.States == null
                    ? parent.States
                    : parent.States.Concat(child.States).ToArray(),
                null
                );
        }
    }
}
