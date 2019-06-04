using System.Collections.Generic;
using System.Linq;

namespace RT.Scheme.Requirements
{
    static class StateRequirements
    {
        public static List<string> Check(State state, Tile tile)
        {
            List<string> errors = new List<string>();

            if (state.Model != null && tile.Models != null && state.Model >= tile.Models.Length)
                errors.Add($"'model' contains index {state.Model}, but length of tile's 'models' only {tile.Models.Length}");

            if (state.Layers != null && tile.Textures != null)
                foreach (uint l in state.Layers)
                    if (l >= tile.Textures.Length)
                        errors.Add($"'layers' contains index {l}, but length of tile's 'textures' only {tile.Textures.Length}");

            return errors;
        }
    }
}
