using Newtonsoft.Json;
using OpenTK;
using RT.Scheme.Converters;
using System.Collections.Generic;

namespace RT.Scheme
{
#pragma warning disable CS0649
    class Model
    {
        [JsonProperty(Required = Required.Always)]
        public readonly List<Vector3> Positions;

        [JsonProperty(Required = Required.Always)]
        public readonly List<Vector3> Normals;

        [JsonProperty(Required = Required.Always)]
        public readonly List<Vector2> TextureMap;

        [JsonProperty(Required = Required.Always)]
        public readonly List<Face> Faces;

        public Model(
            [JsonConverter(typeof(Vector3ListConverter))] List<Vector3> positions,
            [JsonConverter(typeof(Vector3ListConverter))] List<Vector3> normals,
            [JsonConverter(typeof(Vector2ListConverter))] List<Vector2> textureMap,
            List<Face> faces
            )
        {
            Positions = positions;
            Normals = normals;
            TextureMap = textureMap;
            Faces = faces;
        }
    }
#pragma warning enable CS0649
}
