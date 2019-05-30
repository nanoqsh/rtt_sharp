using System.Collections.Generic;

namespace RT.Scheme
{
    class State
    {
        public readonly uint? Model;
        public readonly Dictionary<string, object>? Layers;
        public readonly string[]? Transform;

        public State(uint? model, Dictionary<string, object>? layers, string[]? transform)
        {
            Model = model;
            Layers = layers;
            Transform = transform;
        }

        public readonly static State Empty = new State(null, null, null);

        public static State Inherit(State child, State parent) =>
            new State(
                child.Model ?? parent.Model,
                child.Layers ?? parent.Layers,
                child.Transform ?? parent.Transform
                ); 
    }
}
