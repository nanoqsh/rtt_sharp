using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RT.Scheme;
using System;
using System.IO;

namespace RT
{
    static class Loader
    {
        private static readonly JsonSerializerSettings settings;
        private static readonly DefaultContractResolver contractResolver;

        static Loader()
        {
            contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error,
                ContractResolver = contractResolver
            };
        }

        public static Model LoadModel(string name)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Assets/Models/" + name;

            if (!File.Exists(path))
                throw new Exception($"File {path} is not exists!");

            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Model>(json, settings);
        }
    }
}
