using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Flakcore;
using Microsoft.Xna.Framework;
using Flakcore.Utils;

namespace CallOfHonour.GameObjects
{
    public class Bullet : Sprite
    {
        protected float _restitution;
        protected float _speed;
        protected bool _ignoreGravity;

        public Bullet()
        {
            _restitution = 0;
            _ignoreGravity = true;
            _speed = ConvertUnits.ToSimUnits(50);
            LoadTexture("bullet");
            Visable = false;

            this.addCollisionGroup("bullet");
        }

        public virtual void activate(Vector2 shooterPosition, Vector2 direction)
        {
            Visable = true;
        }
    }
}
