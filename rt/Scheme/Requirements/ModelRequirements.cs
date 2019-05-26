using System.Collections.Generic;

namespace RT.Scheme.Requirements
{
    static class ModelRequirements
    {
        public static List<string> Check(Model model)
        {
            List<string> errors = new List<string>();

            if (model.Faces == null)
                errors.Add("'faces' are required");

            if (model.Positions != null && model.Positions.GetLength(1) != 3)
                errors.Add("'positions' must contain three numbers");

            if (model.Normals != null && model.Normals.GetLength(1) != 3)
                errors.Add("'normals' must contain three numbers");

            if (model.TextureMap != null && model.TextureMap.GetLength(1) != 2)
                errors.Add("'texture_map' must contain two numbers");

            if (model.Faces != null)
                foreach (Face face in model.Faces)
                {
                    errors.AddRange(FaceRequirements.Check(face));

                    if (face.Data != null && face.Data.Positions != null && model.Positions != null)
                        foreach (uint p in face.Data.Positions)
                            if (p >= model.Positions.GetLength(0))
                                errors.Add($"'positions' in 'data' of face contains index {p}, but length of model 'positions' only {model.Positions.GetLength(0)}");

                    if (face.Data != null && face.Data.TextureMap != null && model.TextureMap != null)
                        foreach (uint p in face.Data.TextureMap)
                            if (p >= model.TextureMap.GetLength(0))
                                errors.Add($"'texture_map' in 'data' of face contains index {p}, but length of model 'texture_map' only {model.TextureMap.GetLength(0)}");
                }

            return errors;
        }
    }
}
