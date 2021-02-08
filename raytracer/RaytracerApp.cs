using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using Atamocius.Core;

namespace raytracer
{
    public class RaytracerApp : IApp
    {
        public (uint Width, uint Height) RenderSize { get; }
        public Color ClearColor { get; }

        private readonly ICanvas canvas;

        public RaytracerApp(ICanvas canvas)
        {
            this.canvas = canvas;

            this.RenderSize = canvas.Size;
            this.ClearColor = Color.Black;
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
            this.canvas.Clear(Color.Cyan);
            this.canvas.PutPixel(159, 119, Color.Red);
            this.canvas.PutPixel(161, 119, Color.Red);
            this.canvas.PutPixel(160, 120, Color.Red);
            this.canvas.PutPixel(159, 121, Color.Red);
            this.canvas.PutPixel(161, 121, Color.Red);
            this.canvas.Present(ctx);
        }
    }
}
