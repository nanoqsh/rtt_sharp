using RT.Render;

namespace RT.Converters
{
    static class ModelConverter
    {
        public static Model Convert(Scheme.Model modelData)
        {
            return new Model(new Face[0]);
        }
    }
}
