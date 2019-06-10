using RT.Engine;
using System;
using System.Linq;

namespace RT.Scheme.Converters
{
    class StateConverter
    {
        public static Engine.State Convert(State state, Tile tile)
        {
            Exception undefModel = new Exception("Undefined model!");
            Exception undefTexture = new Exception("Undefined texture!");

            Box box = state.Transform == null
                ? new Box()
                : BoxFromTransform(state.Transform);

            if (tile.Models == null || state.Model == null)
                throw undefModel;

            Render.Model? model = Core.Unit.Resource.LoadModel(tile.Models[(int)state.Model]);

            if (model == null)
                throw undefModel;

            Render.Model tranformed = new Render.Model(
                model.Faces.Select(f => new Render.Face(f, box)).ToArray(),
                model.FullSides
                );

            if (tile.Textures == null)
                throw undefTexture;

            uint[]? layers = state.Layers ?? tile.Default?.Layers;

            if (layers == null)
                throw undefTexture;

            uint[] loaded = layers
                .Select(l => Core.Unit.Atlas.LoadSprite(tile.Textures[l]))
                .ToArray();

            return new Engine.State(tranformed, loaded, box);
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
