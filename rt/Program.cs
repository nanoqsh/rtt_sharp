using RT.Engine;
using RT.Scheme;
using System;

namespace RT
{
    class Program
    {
        static void Main(string[] args)
        {
            // new Core().Init();

            // Model model = Loader.LoadModel("model.json");

            Box box = new Box();
            box
                .Turn(Axis.X)
                .Flip(Axis.X);

            Console.WriteLine(box);

            Console.ReadKey();
        }
    }
}
