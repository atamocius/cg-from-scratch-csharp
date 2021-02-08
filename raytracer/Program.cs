using Atamocius.Core;

namespace raytracer
{
    class Program
    {
        static void Main(string[] args)
        {
            var canvas = new Canvas(320, 240);

            var app = new RaytracerApp(canvas);

            var window = new AppWindow(
                "Raytracer",
                (640, 480),
                app);
            window.Show();
        }
    }
}
