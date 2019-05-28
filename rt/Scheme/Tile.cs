using System.Collections.Generic;

namespace RT.Scheme
{
    class Tile
    {
        public readonly string[]? Models;
        public readonly string[]? Textures;
        public readonly Dictionary<string, string>? Properties;
        public readonly State[]? States;
        public readonly string? Parent;

        public Tile(string[]? models, string[]? textures, Dictionary<string, string>? properties, State[]? states, string? parent)
        {
            Models = models;
            Textures = textures;
            Properties = properties;
            States = states;
            Parent = parent;
        }

        public static Tile InheritTile(Tile parent, Tile child)
        {
            string[]? models = child.Models ?? parent.Models;
            string[]? textures = child.Textures ?? parent.Textures;
            Dictionary<string, string>? properties = child.Properties ?? parent.Properties;

            State[]? states = child.Properties == null
                ? MergeStates(parent.States ?? new State[0], child.States ?? new State[0])
                : child.States;

            return new Tile(models, textures, properties, states, null);
        }

        private static State[] MergeStates(State[] parentStates, State[] childStates)
        {
            Dictionary<string, State> named = new Dictionary<string, State>();
            List<State> result = new List<State>();

            foreach (State state in parentStates)
                if (state.Name == null)
                    result.Add(state);
                else
                    named.Add(state.Name, state);

            foreach (State state in childStates)
                if (state.Name == null)
                    result.Add(state);
                else if (named.ContainsKey(state.Name))
                    named[state.Name] = State.InheritState(named[state.Name], state);
                else
                    named.Add(state.Name, state);

            result.AddRange(named.Values);
            return result.ToArray();
        }
    }
}
