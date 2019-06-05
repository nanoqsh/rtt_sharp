using System;

namespace RT.Render
{
    static class FaceArray
    {
        public static (Vertex[], uint[]) GetIndexedVertexes(this Face[] faces)
        {
            int vertexesCount = 0;
            int indexesCount = 0;

            foreach (Face face in faces)
                switch (face.Vertexes.Length)
                {
                    case 4:
                        vertexesCount += 4;
                        indexesCount += 6;
                        break;

                    case 3:
                        vertexesCount += 3;
                        indexesCount += 3;
                        break;

                    default:
                        throw new Exception("Wrong vertexes length!");
                }

            Vertex[] vertexes = new Vertex[vertexesCount];
            uint[] indexes = new uint[indexesCount];

            int v = 0;
            int n = 0;
            uint index = 0;
            foreach (Face face in faces)
                switch (face.Vertexes.Length)
                {
                    case 4:
                        vertexes[v++] = face.Vertexes[0];
                        vertexes[v++] = face.Vertexes[1];
                        vertexes[v++] = face.Vertexes[2];
                        vertexes[v++] = face.Vertexes[3];
                        indexes[n++] = index;     // 0
                        indexes[n++] = index + 1; // 1
                        indexes[n++] = index + 2; // 2
                        indexes[n++] = index;     // 0
                        indexes[n++] = index + 2; // 2
                        indexes[n++] = index + 3; // 3
                        index += 4;
                        break;

                    case 3:
                        vertexes[v++] = face.Vertexes[0];
                        vertexes[v++] = face.Vertexes[1];
                        vertexes[v++] = face.Vertexes[2];
                        indexes[n++] = index;     // 0
                        indexes[n++] = index + 1; // 1
                        indexes[n++] = index + 2; // 2
                        index += 3;
                        break;

                    default:
                        throw new Exception("Wrong vertexes length!");
                }

            return (vertexes, indexes);
        }
    }
}
