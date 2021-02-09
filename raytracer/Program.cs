using Atamocius.Core;

namespace raytracer
{
    class Program
    {
        static void Main(string[] args)
        {
            var canvas = new Canvas(600, 600);

            var app = new RaytracerApp(canvas);

            var window = new AppWindow(
                "Raytracer",
                (600, 600),
                app);
            window.Show();
        }
    }
}
