using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Bunker_Hunter.GameObjects.Level
{
    class Block : Node
    {
        public static Texture2D Graphic { get; set; }

        public Block()
        {
            this.addCollisionGroup("tilemap");
            this.Collidable = false;
            this.Immovable = true;
            this.Width = Level.BLOCK_WIDTH;
            this.Height = Level.BLOCK_HEIGHT;
        }

        public override Flakcore.Utils.BoundingRectangle GetBoundingBox()
        {
            return base.GetBoundingBox();
        }

        public Rectangle GetSourceRectangle()
        {
            return new Rectangle(32, 0, 32, 32);
        }
    }
}
