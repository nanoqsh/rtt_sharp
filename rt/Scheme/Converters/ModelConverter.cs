using RT.Engine;
using System.Linq;

namespace RT.Scheme.Converters
{
    static class ModelConverter
    {
        public static Render.Model Convert(Model model)
        {
            Render.Face[] faces = model.Faces == null
                ? new Render.Face[0]
                : model.Faces.Select(f => FaceConverter.Convert(f, model)).ToArray();

            Side fullSides = model.FullSides == null
                ? Side.None
                : SideMethods.FromString(model.FullSides);

            return new Render.Model(faces, fullSides);
        }
    }
}
