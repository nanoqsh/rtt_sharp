using System.Collections.Generic;

namespace RT.Scheme
{
    class State
    {
        public readonly uint? Model;
        public readonly Dictionary<string, uint>? Layers;
        public readonly Dictionary<string, object>? Values;
        public readonly string[]? Transform;
        public readonly string? Name;

        public State(uint? model, Dictionary<string, uint>? layers, Dictionary<string, object>? values, string[]? transform, string? name)
        {
            Model = model;
            Layers = layers;
            Values = values;
            Transform = transform;
            Name = name;
        }

        public static State InheritState(State parent, State child)
        {
            uint? model = child.Model ?? parent.Model;
            Dictionary<string, uint>? layers = child.Layers ?? parent.Layers;
            Dictionary<string, object>? values = child.Values ?? parent.Values;
            string[]? transform = child.Transform ?? parent.Transform;

            return new State(model, layers, values, transform, child.Name);
        }
    }
}
