using OpenTK;
using System.Collections.Generic;

namespace RT.Scheme.Converters
{
    static class Converter
    {
        public static Vector3 ConvertVector3(float[] points) =>
            new Vector3(points[0], points[1], points[2]);

        public static Vector2 ConvertVector2(float[] points) =>
            new Vector2(points[0], points[1]);

        public static List<Vector3> ConvertVector3List(float[,] points)
        {
            int len = points.GetLength(0);
            List<Vector3> result = new List<Vector3>(len);

            for (int i = 0; i < len; ++i)
                result.Add(new Vector3(
                    points[i, 0],
                    points[i, 1],
                    points[i, 2]
                    ));

            return result;
        }

        public static List<Vector2> ConvertVector2List(float[,] points)
        {
            int len = points.GetLength(0);
            List<Vector2> result = new List<Vector2>(len);

            for (int i = 0; i < len; ++i)
                result.Add(new Vector2(
                    points[i, 0],
                    points[i, 1]
                    ));

            return result;
        }

        public static Vector3 Vector3FromData(float[,] data, int index) =>
            new Vector3(data[index, 0], data[index, 1], data[index, 2]);

        public static Vector2 Vector2FromData(float[,] data, int index) =>
            new Vector2(data[index, 0], data[index, 1]);
    }
}
