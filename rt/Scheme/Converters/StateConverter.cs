using RT.Engine;
using System;
using System.Collections.Generic;

namespace RT.Scheme.Converters
{
    class StateConverter
    {
        public static Engine.State Convert(State state, Tile tile)
        {
            Exception undefModel = new Exception("Undefined model!");
            Box box = state.Transform == null
                ? new Box()
                : BoxFromTransform(state.Transform);

            Dictionary<string, object> values = state.Values ?? new Dictionary<string, object>();

            if (tile.Models == null || state.Model == null || state.Model >= tile.Models.Length)
                throw undefModel;

            Render.Model? model = Core.Inst.Resource.LoadModel(tile.Models[(int)state.Model]);

            if (model == null)
                throw undefModel;

            // state.Layers

            return new Engine.State(model, null, values, box);
        }

        private static Box BoxFromTransform(string[] transform)
        {
            Box box = new Box();

            foreach (string t in transform)
                switch (t)
                {
                    case "flip_x": box.Flip(Axis.X); break;
                    case "flip_y": box.Flip(Axis.Y); break;
                    case "flip_z": box.Flip(Axis.Z); break;
                    case "turn_x": box.Turn(Axis.X); break;
                    case "turn_y": box.Turn(Axis.Y); break;
                    case "turn_z": box.Turn(Axis.Z); break;
                };

            return box;
        }
    }
}
