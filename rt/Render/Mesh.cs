using OpenTK.Graphics.OpenGL4;
using RT.Engine;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace RT.Render
{
    class Mesh : IDisposable
    {
        public readonly int VAOIndex;
        private readonly int drawCount;

        public Mesh(State state, ShaderProgram shader)
            : this(ToFaces(state), shader)
        { }

        public Mesh(Face[] faces, ShaderProgram shader)
        {
            (Vertex[] vertices, uint[] indexes) = faces.GetIndexedVertexes();
            drawCount = indexes.Length;

            VAOIndex = GL.GenVertexArray();
            GL.BindVertexArray(VAOIndex);

            int arrayBufferIndex = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, arrayBufferIndex);

            GL.BufferData(
                BufferTarget.ArrayBuffer,
                vertices.Length * Marshal.SizeOf(typeof(Vertex)),
                vertices,
                BufferUsageHint.StaticDraw
                );

            foreach (Attribute attribute in Vertex.GetAttributes(shader))
            {
                GL.EnableVertexAttribArray(attribute.Index);
                GL.VertexAttribPointer(
                    attribute.Index,
                    attribute.SizeOfElements,
                    VertexAttribPointerType.Float,
                    false,
                    attribute.StrideOfElements * sizeof(float),
                    attribute.OffsetOfElements * sizeof(float)
                    );
            }

            int elementBufferIndex = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferIndex);

            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                indexes.Length * sizeof(uint),
                indexes,
                BufferUsageHint.StaticDraw
                );

            GL.BindVertexArray(0);
        }

        public void Dispose()
        {
            GL.BindVertexArray(0);
            GL.DeleteVertexArray(VAOIndex);
        }

        public void Draw()
        {
            GL.BindVertexArray(VAOIndex);
            GL.DrawElements(BeginMode.Triangles, drawCount, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }

        private static Face[] ToFaces(State state)
        {
            Face[] result = new Face[state.Model.Faces.Length];

            for (int i = 0; i < result.Length; ++i)
                result[i] = new Face(
                    state.Model.Faces[i],
                    Core.Unit.SpriteMap.GetUV(
                        state.Layers[state.Model.Faces[i].Layer],
                        state.Model.Faces[i].Vertexes.Select(v => v.TextureMap).ToArray()
                        )
                    );

            return result;
        }
    }
}
