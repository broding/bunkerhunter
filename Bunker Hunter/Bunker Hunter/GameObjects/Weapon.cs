using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Flakcore.Display;
using CallOfHonour.GameObjects;
using Bunker_Hunker.GameObjects;

namespace Bunker_Hunter.GameObjects
{
    public class Weapon : Node
    {
        public Character User;

        private Layer BulletLayer;
        private WeaponType WeaponType;
        private List<Bullet> Bullets;

        private int FireRate;

        public Weapon(Layer bulletLayer)
        {
            this.WeaponType = new WeaponType();
            this.Bullets = new List<Bullet>();
            this.BulletLayer = bulletLayer;

            this.FireRate = 0;

            this.InitializeBullets();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.FireRate += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        private void InitializeBullets()
        {
            for (int i = 0; i < 100; i++)
            {
                Bullet bullet = new Bullet(this.BulletLayer, new BulletType());
                this.Bullets.Add(bullet);
                this.BulletLayer.addChild(bullet);
            }
        }

        public void Fire(Vector2 position, Facing facing, Character user)
        {
            if (this.FireRate < this.WeaponType.FireRate)
                return;

            this.FireRate = 0;

            foreach (Bullet bullet in Bullets)
            {
                if (bullet.Dead)
                {
                    bullet.Fire(position, facing, user);
                    return;
                }
            }
        }
    }
}
