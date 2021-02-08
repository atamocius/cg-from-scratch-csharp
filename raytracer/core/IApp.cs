using SFML.Graphics;

namespace Atamocius.Core
{
    public interface IApp
    {
        (uint Width, uint Height) RenderSize { get; }
        Color ClearColor { get; }

        void Start(RenderWindow window);
        void ProcessInput(RenderWindow window);
        void Render(RenderTarget ctx);
        void Update(in float dt);
    }
}
