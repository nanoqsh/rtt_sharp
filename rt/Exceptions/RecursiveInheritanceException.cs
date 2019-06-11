using System.Collections.Generic;

namespace RT.Exceptions
{
    class RecursiveInheritanceException : LoaderException
    {
        public RecursiveInheritanceException(string file, List<string> loaded)
            : base(file, new List<string> {
                    $"Recursive inheritance: {string.Join(" -> ", loaded)} -> {file}"
                })
        { }
    }
}
