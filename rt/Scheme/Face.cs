using Newtonsoft.Json;
using System.Collections.Generic;

namespace RT.Scheme
{
#pragma warning disable CS0649
    class Face
    {
        [JsonProperty(Required = Required.Always)]
        public readonly List<uint[]> Data;

        [JsonProperty(Required = Required.Always)]
        public readonly string Contact;

        [JsonProperty(Required = Required.Always)]
        public readonly uint Layer;

        public Face(List<uint[]> data, string contact, uint layer)
        {
            Data = data;
            Contact = contact;
            Layer = layer;
        }
    }
#pragma warning enable CS0649
}
