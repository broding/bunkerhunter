using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flakcore.Display
{
    public class Layer : Node
    {
        public Layer()
            : base()
        {
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Matrix parentTransform)
        {
            base.Draw(spriteBatch, parentTransform);
        }
    }
}
