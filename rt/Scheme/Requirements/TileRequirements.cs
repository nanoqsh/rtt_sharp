using System.Collections.Generic;
using System.Linq;

namespace RT.Scheme.Requirements
{
    static class TileRequirements
    {
        public static List<string> Check(Tile tile)
        {
            List<string> errors = new List<string>();

            if (tile.Models == null || tile.Models.Length == 0)
                errors.Add("'models' is required");

            if (tile.States == null || tile.States.Length == 0)
                errors.Add("'states' is required");

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
