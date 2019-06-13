using System;

namespace RT
{
    static class Ref
    {
        public static readonly string Path = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string Shaders = Path + "Shaders/";
        public static readonly string Assets = Path + "Assets/";
        public static readonly string Models = Assets + "Models/";
        public static readonly string Tiles = Assets + "Tiles/";
        public static readonly string Textures = Assets + "Textures/";
        public static readonly string Font = Assets + "Font/";
    }
}
