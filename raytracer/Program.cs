using Atamocius.Core;

namespace raytracer
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new RaytracerApp(320, 240);

            var window = new AppWindow(
                "Raytracer",
                (640, 480),
                app);
            window.Show();
        }
    }
}
