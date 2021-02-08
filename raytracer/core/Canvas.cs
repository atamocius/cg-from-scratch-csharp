using SFML.Graphics;

namespace Atamocius.Core
{
    public class Canvas : ICanvas
    {
        private readonly (uint Width, uint Height) renderSize;
        private readonly byte[] colorBuffer;
        private readonly Sprite backbufferSprite;
        private readonly Texture backbuffer;

        public Canvas(uint renderWidth, uint renderHeight)
        {
            this.renderSize = (renderWidth, renderHeight);

            this.colorBuffer = new byte[renderWidth * renderHeight * 4];
            this.backbuffer = new Texture(renderWidth, renderHeight)
            {
                Repeated = false
            };
            this.backbufferSprite = new Sprite(this.backbuffer);
        }

        public void Clear(in Color color)
        {
            for (var i = 0; i < this.colorBuffer.Length; i += 4)
            {
                this.colorBuffer[i] = color.R;
                this.colorBuffer[i + 1] = color.G;
                this.colorBuffer[i + 2] = color.B;
                this.colorBuffer[i + 3] = 255;
            }
        }

        public void PutPixel(in int x, in int y, in Color color)
        {
            var i = (x + y * this.renderSize.Width) * 4;

            this.colorBuffer[i] = color.R;
            this.colorBuffer[i + 1] = color.G;
            this.colorBuffer[i + 2] = color.B;
            this.colorBuffer[i + 3] = 255;
        }

        public void Present(RenderTarget ctx)
        {
            this.backbuffer.Update(this.colorBuffer);

            ctx.Draw(this.backbufferSprite);
        }
    }
}
