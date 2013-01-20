using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Utils;
using Flakcore.Display;
using Flakcore;
using Microsoft.Xna.Framework;

namespace Bunker_Hunter.UI
{
    internal class DamageText : Node, IPoolable
    {
        public int PoolIndex { get; set; }
        public Action<int> ReportDeadToPool { get; set; }

        public DamageText()
        {
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, ParentNode parentNode)
        {
            base.Draw(spriteBatch, parentNode);

        }

        protected override void DrawCall(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, ParentNode parentNode)
        {
            spriteBatch.DrawString(GameManager.fontDefault, "152", this.Position, Color.White);
        }
    }
}
