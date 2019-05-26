using System;
using System.Collections.Generic;

namespace RT.Exceptions
{
    class LoaderException : Exception
    {
        public LoaderException(string file, List<string> errors)
            : base($"Loader cannot load file {file}! Load errors: {string.Join("; ", errors)}")
        { }
    }
}
