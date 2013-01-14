using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Flakcore.Display.Level.LevelObjects
{
    class Ladder : LevelObject
    {
        private int LadderHeight;

        public Ladder(int height) : base()
        {
            this.LadderHeight = height;

            this.LoadTexture("ladder");
            this.AddCollisionGroup("ladder");
            this.Height = this.LadderHeight * Level.BLOCK_HEIGHT;
            this.Immovable = true;
        }

        protected override void DrawCall(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Vector2 position)
        {
            for (int y = 0; y < this.LadderHeight; y++)
            {
                base.DrawCall(spriteBatch, position);
                position.Y += this.Texture.Height;
            }
        }
    }
}
