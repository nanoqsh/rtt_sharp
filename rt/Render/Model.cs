using RT.Engine;
using System.Collections.Generic;

namespace RT.Render
{
    class Model
    {
        public readonly Face[] Faces;
        public readonly Side FullSides;

        public Model(Face[] faces, Side fullSides)
        {
            Faces = faces;
            FullSides = fullSides;
        }
    }
}
