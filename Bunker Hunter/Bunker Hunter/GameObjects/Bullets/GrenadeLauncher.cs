using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Utils;

namespace CallOfHonour.GameObjects.Bullets
{
    class GrenadeLauncher : Bullet
    {
        public GrenadeLauncher() : base()
        {

            this._ignoreGravity = false;
            this._restitution = 0.4f;
            this._speed = ConvertUnits.ToSimUnits(300);
            this.addCollisionGroup("grenadeLauncher");
        }

        public override void activate(Microsoft.Xna.Framework.Vector2 shooterPosition, Microsoft.Xna.Framework.Vector2 direction)
        {
            direction.Y -= 1f;
            base.activate(shooterPosition, direction);
        }
    }
}
