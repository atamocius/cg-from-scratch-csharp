using SFML.Graphics;

namespace Atamocius.Core
{
    public interface ICanvas
    {
        (ushort Width, ushort Height) Size { get; }
        void Clear(in Color color);
        void Present(RenderTarget ctx);
        void PutPixel(in int x, in int y, in Color color);
        void Update();
    }
}
