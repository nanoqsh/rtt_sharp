using System.Collections.Generic;

namespace RT.Scheme.Requirements
{
    static class StateRequirements
    {
        public static List<string> Check(State state, Tile tile)
        {
            List<string> errors = new List<string>();

            if (state.Model == null)
                errors.Add("each state must contain 'model'");

            if (state.Model != null && tile.Models != null && state.Model >= tile.Models.Length)
                errors.Add($"'model' contains index {state.Model}, but length of tile's 'models' only {tile.Models.Length}");

            return errors;
        }
    }
}
