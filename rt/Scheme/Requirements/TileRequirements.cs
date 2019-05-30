using System.Collections.Generic;

namespace RT.Scheme.Requirements
{
    static class TileRequirements
    {
        public static List<string> Check(Tile tile)
        {
            List<string> errors = new List<string>();

            if (tile.Models == null)
                errors.Add("'models' are required");

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
