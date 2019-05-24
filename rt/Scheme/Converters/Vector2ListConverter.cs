using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenTK;

namespace RT.Scheme.Converters
{
    class Vector2ListConverter : JsonConverter<List<Vector2>>
    {
        public override List<Vector2> ReadJson(JsonReader reader, Type objectType, List<Vector2> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartArray)
                throw MakeException("Token type must be array", reader);

            JArray list = JArray.Load(reader);
            List<Vector2> result = new List<Vector2>();

            foreach (JToken vector in list)
            {
                if (vector.Type != JTokenType.Array || !(vector is JArray))
                    throw MakeException("Token type must be array of numbers", reader);

                if ((vector as JArray)!.Count != 2)
                    throw MakeException("Array length must be 2", reader);

                float x = (vector as JArray)![0].Value<float>();
                float y = (vector as JArray)![1].Value<float>();

                result.Add(new Vector2(x, y));
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, List<Vector2> value, JsonSerializer serializer)
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
