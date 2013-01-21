using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Utils;
using Flakcore.Display;
using Flakcore;
using Microsoft.Xna.Framework;
using Flakcore.Display.Activities;

namespace Bunker_Hunter.UI
{
    internal class DamageText : Sprite, IPoolable
    {
        public int PoolIndex { get; set; }
        public Action<int> ReportDeadToPool { get; set; }

        public DamageText()
        {
        }

        public override void Activate()
        {
            base.Activate();

            this.Scale.X = 1.5f;
            this.Scale.Y = 1.5f;
            this.Alpha = 1f;

            Activity scale = new ScaleTo(this, 200, new Vector2(0.9f, 0.9f));
            Activity alpha = new AlphaTo(this, 1000, 0);
            Sequence sequence = new Sequence(this);

            sequence.AddActivity(scale);
            sequence.AddActivity(alpha);

            this.AddActivity(sequence, true);
        }

        protected override void DrawCall(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, ParentNode parentNode)
        {
            spriteBatch.DrawString(GameManager.fontDefault, "152", this.Position, this.Color * this.Alpha, this.Rotation, this.Origin, this.Scale, this.SpriteEffects, this.GetParentDepth());
        }
    }
}
