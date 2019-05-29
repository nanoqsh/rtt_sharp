using RT.Render;
using RT.Scheme.Converters;
using System.Collections.Generic;

namespace RT
{
    class Resource
    {
        private readonly Dictionary<string, Model> models;

        public Resource()
        {
            models = new Dictionary<string, Model>();
        }

        public Model LoadModel(string file)
        {
            if (models.TryGetValue(file, out Model result))
                return result;

            Model model = ModelConverter.Convert(Loader.LoadModel(file));
            models.Add(file, model);
            return model;
        }
    }
}
