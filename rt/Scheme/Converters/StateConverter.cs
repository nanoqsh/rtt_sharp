using RT.Engine;
using System;

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

            if (tile.Models == null || state.Model == null)
                throw undefModel;

            Render.Model? model = Core.Unit.Resource.LoadModel(tile.Models[(int)state.Model]);

            if (model == null)
                throw undefModel;

            uint[] layers = new uint[0];

            return new Engine.State(model, layers, box);
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
