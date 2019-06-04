using System.Collections.Generic;
using System.Linq;

namespace RT.Scheme.Requirements
{
    static class TileRequirements
    {
        public static List<string> Check(Tile tile)
        {
            List<string> errors = new List<string>();

            if (tile.Models == null)
                errors.Add("'models' are required");

            bool hasDefaultModel = tile.Default != null && tile.Default.Model != null;
            bool allStatesHaveModel = tile.States != null && tile.States.All(s => s.Model != null);
            if (!hasDefaultModel && !allStatesHaveModel)
                errors.Add("specify model index in 'default' or 'states' is required");

            if (tile.Default != null)
                errors.AddRange(StateRequirements.Check(tile.Default, tile));

            if (tile.States != null)
                foreach (State st in tile.States)
                    errors.AddRange(StateRequirements.Check(st, tile));

            if (tile.AddStates != null)
                foreach (State st in tile.AddStates)
                    errors.AddRange(StateRequirements.Check(st, tile));

            return errors;
        }
    }
}
