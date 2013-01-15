using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Flakcore.Display;
using CallOfHonour.GameObjects;
using Bunker_Hunker.GameObjects;
using Bunker_Hunter.Types;
using Flakcore;

namespace Bunker_Hunter.Models
{
    public class Weapon : Node
    {
        public Character User;

        private Layer BulletLayer;
        private WeaponType WeaponType;

        private int FireRate;

        public Weapon(Layer bulletLayer)
        {
            this.WeaponType = new WeaponType();
            this.BulletLayer = bulletLayer;

            this.FireRate = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.FireRate += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Fire(Vector2 position, Facing facing, Character user)
        {
            if (this.FireRate < this.WeaponType.FireRate)
                return;

            Bullet bullet = this.WeaponType.BulletType.Pool.New();
            bullet.Fire(position, facing, user);
            GameManager.BulletLayer.AddChild(bullet);

            this.FireRate = 0;

            
        }
    }
}
