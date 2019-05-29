namespace RT.Scheme
{
    class FaceData
    {
        public readonly uint[]? Positions;
        public readonly uint[]? TextureMap;
        public readonly uint? Normal;

        public FaceData(uint[] positions, uint[] textureMap, uint? normal)
        {
            Positions = positions;
            TextureMap = textureMap;
            Normal = normal;
        }
    }
}
