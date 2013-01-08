using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Flakcore.Display
{
    class TiledSprite : Sprite
    {
        public int TiledWidth;
        public int TiledHeight;

        public TiledSprite(int width, int height)
        {
            this.TiledWidth = width;
            this.TiledHeight = height;
        }

        public TiledSprite()
        {
        }

        public override void LoadTexture(Texture2D texture, int width, int height)
        {
            base.LoadTexture(texture, width, height);

            this.Width = width * this.TiledWidth;
            this.Height = height * this.TiledHeight;
        }

        protected override void DrawCall(SpriteBatch spriteBatch, Vector2 position)
        {
            int amountX = this.TiledWidth / this.Texture.Width;
            int amountY = this.TiledHeight / this.Texture.Height;

            for (int x = 0; x < amountX; x++)
            {
                for (int y = 0; y < amountY; y++)
                {
                    Vector2 newPosition = position + new Vector2(x * this.Texture.Width * this.Scale.X, y * this.Texture.Height * this.Scale.Y);
                    base.DrawCall(spriteBatch, newPosition); 
                }
            }
        }
    }
}
