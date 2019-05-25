using System;

namespace RT.Engine
{
    static class Undefined<T>
    {
        public static Exception Error =>
            new Exception($"Undefined {typeof(T).Name}!");
    }
}
