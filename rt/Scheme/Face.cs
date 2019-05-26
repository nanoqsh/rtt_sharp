namespace RT.Scheme
{
    class Face
    {
        public readonly float[,]? Positions;
        public readonly float[,]? TextureMap;
        public readonly float[]? Normal;
        public readonly FaceData? Data;
        public readonly string? Contact;
        public readonly uint? Layer;

        public Face(float[,]? positions, float[,]? textureMap, float[]? normal, FaceData? data, string? contact, uint? layer)
        {
            Positions = positions;
            TextureMap = textureMap;
            Normal = normal;
            Data = data;
            Contact = contact;
            Layer = layer;
        }
    }
}
