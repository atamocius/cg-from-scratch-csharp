using SFML.Window;
using SFML.Graphics;
using Atamocius.Core;

namespace raytracer
{
    public class RaytracerApp : IApp
    {
        public (uint Width, uint Height) RenderSize { get; }
        public Color ClearColor { get; }

        private readonly ICanvas canvas;

        private readonly Scene scene;

        public RaytracerApp(ICanvas canvas)
        {
            this.canvas = canvas;

            this.RenderSize = canvas.Size;
            this.ClearColor = Color.Black;

            this.scene = new Scene(canvas);
        }

        public void Start(RenderWindow window)
        {
        }

        public void ProcessInput(RenderWindow window)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                window.Close();
            }
        }

        public void Update(in float dt)
        {
        }

        public void Render(RenderTarget ctx)
        {
            this.canvas.Clear(Color.Black);

            this.scene.Trace();

            this.canvas.Present(ctx);
        }
    }
}
