using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Atamocius.Core
{
    public class AppWindow
    {
        public string Title { get; }
        public (uint Width, uint Height) WindowSize { get; }
        private readonly IApp app;

        public AppWindow(
            string title,
            (uint Width, uint Height) windowSize,
            IApp app)
        {
            this.Title = title;
            this.WindowSize = windowSize;

            this.app = app;
        }

        public void Show()
        {
            var (pixelW, pixelH) = this.app.RenderSize;
            var (winW, winH) = this.WindowSize;

            var mode = new VideoMode(pixelW, pixelH);

            var window = new RenderWindow(mode, this.Title);
            window.Size = new Vector2u(winW, winH);
            window.Closed += (_, _) => window.Close();
            window.Resized += (_, _) => this.ScaleWindowContents(window, mode);

            this.RunLoop(window, 60, 1);
        }

        private void RunLoop(
            RenderWindow window,
            uint targetFPS,
            float idleThreshold)
        {
            var clock = new Clock();
            Func<float> getCurrentTime = () => clock.ElapsedTime.AsSeconds();

            var secsPerUpdate = 1f / (float)targetFPS;
            var current = 0f;
            var elapsed = 0f;

            var previous = getCurrentTime();
            var lag = 0f;

            this.app.Start(window);

            while (window.IsOpen)
            {
                window.DispatchEvents();

                current = getCurrentTime();
                elapsed = current - previous;
                previous = current;

                if (elapsed > idleThreshold)
                {
                    continue;
                }

                lag += elapsed;

                this.app.ProcessInput(window);

                while (lag >= secsPerUpdate)
                {
                    this.app.Update(secsPerUpdate);
                    lag -= secsPerUpdate;
                }

                window.Clear(this.app.ClearColor);

                this.app.Render(window);

                window.Display();
            }
        }

        private void ScaleWindowContents(RenderWindow window, VideoMode mode)
        {
            var vw = window.DefaultView;
            vw.Viewport = ResizeViewportMaintainAspectRatio(
                mode.Width,
                mode.Height,
                window.Size.X,
                window.Size.Y);
            window.SetView(vw);
        }

        private static FloatRect ResizeViewportMaintainAspectRatio(
            float pixelWidth,
            float pixelHeight,
            float windowWidth,
            float windowHeight)
        {
            var screenWidth = windowWidth / pixelWidth;
            var screenHeight = windowHeight / pixelHeight;

            var newLeft = 0f;
            var newTop = 0f;
            var newWidth = 1f;
            var newHeight = 1f;

            if (screenWidth > screenHeight)
            {
                newWidth = screenHeight / screenWidth;
                newLeft = (1f - newWidth) * 0.5f;
            }
            else
            {
                newHeight = screenWidth / screenHeight;
                newTop = (1f - newHeight) * 0.5f;
            }

            return new FloatRect(newLeft, newTop, newWidth, newHeight);
        }
    }
}
