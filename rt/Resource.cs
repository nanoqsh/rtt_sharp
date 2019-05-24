using System;
using System.IO;

namespace RT
{
    static class Resource
    {
        public static string LoadText(string file)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + file;

            if (!File.Exists(path))
                throw new FileNotFoundException($"File {path} not found!");

            return File.ReadAllText(path);
        }
    }
}
