using SFML.Graphics;

namespace Atamocius.Core
{
    public class Canvas : ICanvas
    {
        public (ushort Width, ushort Height) Size { get; }

        private readonly byte[] colorBuffer;
        private readonly Sprite backbufferSprite;
        private readonly Texture backbuffer;

        public Canvas(ushort renderWidth, ushort renderHeight)
        {
            this.Size = (renderWidth, renderHeight);

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
            var cx = this.Size.Width / 2 + x;
            var cy = this.Size.Height / 2 - y - 1;

            if (cx < 0 || cx >= this.Size.Width ||
                cy < 0 || cy >= this.Size.Height)
            {
                return;
            }

            var i = (cx + cy * this.Size.Width) * 4;

            this.colorBuffer[i] = color.R;
            this.colorBuffer[i + 1] = color.G;
            this.colorBuffer[i + 2] = color.B;
            this.colorBuffer[i + 3] = 255;
        }

        public void Update()
        {
            this.backbuffer.Update(this.colorBuffer);
        }

        public void Present(RenderTarget ctx)
        {
            ctx.Draw(this.backbufferSprite);
        }
    }
}
