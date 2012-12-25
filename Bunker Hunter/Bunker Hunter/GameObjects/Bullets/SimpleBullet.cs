using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Utils;

namespace CallOfHonour.GameObjects.Bullets
{
    class SimpleBullet : Bullet
    {
        public SimpleBullet() : base()
        {

            this._ignoreGravity = true;
            this._restitution = 0f;
            this._speed = ConvertUnits.ToSimUnits(165);
            this.addCollisionGroup("simpleBullet");
        }
    }
}
