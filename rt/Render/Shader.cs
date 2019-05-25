using System;
using OpenTK.Graphics.OpenGL4;

namespace RT.Render
{
    class Shader
    {
        public readonly int Index;
        public readonly string Name;

        public Shader(string name, ShaderType type)
        {
            Name = name;

            Index = GL.CreateShader(type);
            GL.ShaderSource(Index, Loader.LoadText(Ref.Shaders + name));
            GL.CompileShader(Index);

            GL.GetShader(Index, ShaderParameter.CompileStatus, out int isCompiled);

            if (isCompiled == 0)
                throw new Exception($"Shader {name} compile: {GL.GetShaderInfoLog(Index)}");
        }
    }
}
