using System.Collections.Generic;

namespace RT.Scheme.Requirements
{
    static class FaceRequirements
    {
        public static List<string> Check(Face face, Model model)
        {
            List<string> errors = new List<string>();

            if (face.Positions == null && face.Data != null && face.Data.Positions == null)
                errors.Add("each face must contain 'positions' or 'data' with 'positions'");

            if (face.TextureMap == null && face.Data != null && face.Data.TextureMap == null)
                errors.Add("each face must contain 'texture_map' or 'data' with 'texture_map'");

            if (face.Positions != null && face.Positions.GetLength(1) != 3)
                errors.Add("'positions' in 'faces' must contain three numbers");

            if (face.Normal != null && face.Normal.Length != 3)
                errors.Add("'normal' in 'faces' must contain three numbers");

            if (face.TextureMap != null && face.TextureMap.GetLength(1) != 2)
                errors.Add("'texture_map' in 'faces' must contain two numbers");

            if (face.Data != null && face.Data.Positions != null && face.Data.Positions.Length != 3 && face.Data.Positions.Length != 4)
                errors.Add("'positions' in 'data' of face must contain 3 or 4 numbers");

            if (face.Data != null && face.Data.Positions != null && face.Data.TextureMap != null && face.Data.Positions.Length != face.Data.TextureMap.Length)
                errors.Add("'positions' and 'texture_map' in 'data' of face must contain the same amount of numbers");

            if (face.Data != null && face.Data.Positions != null && model.Positions != null)
                foreach (uint p in face.Data.Positions)
                    if (p >= model.Positions.GetLength(0))
                        errors.Add($"'positions' in 'data' of face contains index {p}, but length of model 'positions' only {model.Positions.GetLength(0)}");

            if (face.Data != null && face.Data.TextureMap != null && model.TextureMap != null)
                foreach (uint p in face.Data.TextureMap)
                    if (p >= model.TextureMap.GetLength(0))
                        errors.Add($"'texture_map' in 'data' of face contains index {p}, but length of model 'texture_map' only {model.TextureMap.GetLength(0)}");

            return errors;
        }
    }
}
