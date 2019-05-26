using OpenTK;
using RT.Engine;
using System.Collections.Generic;

namespace RT.Scheme.Converters
{
    static class FaceConverter
    {
        public static Render.Face Convert(Face face, Model model)
        {
            List<Vector3> positions = face.Positions == null
                ? new List<Vector3>()
                : Converter.ConvertVector3List(face.Positions);

            List<Vector2> textureMap = face.TextureMap == null
                ? new List<Vector2>()
                : Converter.ConvertVector2List(face.TextureMap);

            Vector3? normal = face.Normal == null
                ? null
                : (Vector3?)Converter.ConvertVector3(face.Normal);

            Side contact = face.Contact == null
                ? Side.None
                : SideMethods.FromString(face.Contact);

            if (face.Data != null)
            {
                AddData(face.Data, model.Positions, model.TextureMap, positions, textureMap);

                if (face.Data.Normal != null && model.Normals != null)
                    normal = Converter.Vector3FromData(model.Normals, (int)face.Data.Normal);
            }

            return new Render.Face(positions.ToArray(), textureMap.ToArray(), face.Layer ?? 0, contact, normal);
        }

        private static void AddData(
            FaceData data,
            float[,]? positionsData,
            float[,]? textureMapData,
            List<Vector3> positions,
            List<Vector2> textureMap
            )
        {
            if (data.Positions != null && positionsData != null)
                foreach (uint i in data.Positions)
                    positions.Add(Converter.Vector3FromData(positionsData, (int)i));

            if (data.TextureMap != null && textureMapData != null)
                foreach (uint i in data.TextureMap)
                    textureMap.Add(Converter.Vector2FromData(textureMapData, (int)i));
        }
    }
}
