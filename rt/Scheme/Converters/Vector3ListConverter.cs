using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenTK;

namespace RT.Scheme.Converters
{
    class Vector3ListConverter : JsonConverter<List<Vector3>>
    {
        public override List<Vector3> ReadJson(JsonReader reader, Type objectType, List<Vector3> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartArray)
                throw MakeException("Token type must be array", reader);
            
            JArray list = JArray.Load(reader);
            List<Vector3> result = new List<Vector3>();

            foreach (JToken vector in list)
            {
                if (vector.Type != JTokenType.Array || !(vector is JArray))
                    throw MakeException("Token type must be array of numbers", reader);

                if ((vector as JArray)!.Count != 3)
                    throw MakeException("Array length must be 3", reader);
                
                float x = (vector as JArray)![0].Value<float>();
                float y = (vector as JArray)![1].Value<float>();
                float z = (vector as JArray)![2].Value<float>();

                result.Add(new Vector3(x, y, z));
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, List<Vector3> value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private JsonReaderException MakeException(string message, JsonReader reader)
        {
            if (reader is JsonTextReader textReader)
                return new JsonReaderException(
                    message,
                    textReader.Path,
                    textReader.LineNumber,
                    textReader.LinePosition,
                    null
                    );

            return new JsonReaderException(message);
        }
    }
}
