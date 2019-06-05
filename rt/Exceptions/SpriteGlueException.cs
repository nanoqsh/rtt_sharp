using System;

namespace RT.Exceptions
{
    class SpriteGlueException : Exception
    {
        public SpriteGlueException(string file, int expected, int got)
            : base($"Wrong sprite size! Expected {expected}, but got {got} in sprite {file}.")
        { }
    }
}
