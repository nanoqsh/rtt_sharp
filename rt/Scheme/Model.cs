namespace RT.Scheme
{
    class Model
    {
        public readonly float[,]? Positions;
        public readonly float[,]? Normals;
        public readonly float[,]? TextureMap;
        public readonly Face[]? Faces;

        public Model(float[,]? positions, float[,]? normals, float[,]? textureMap, Face[]? faces)
        {
            Positions = positions;
            Normals = normals;
            TextureMap = textureMap;
            Faces = faces;
        }
    }
}
