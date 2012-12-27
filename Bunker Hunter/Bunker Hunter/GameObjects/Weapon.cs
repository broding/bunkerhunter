using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Flakcore.Display;
using CallOfHonour.GameObjects;

namespace Bunker_Hunter.GameObjects
{
    public class Weapon : Node
    {
        private WeaponType WeaponType;
        private List<Bullet> Bullets;

        private int FireRate;

        public Weapon(Node bulletNode)
        {
            this.WeaponType = new WeaponType();
            this.Bullets = new List<Bullet>();

            this.FireRate = 0;

            this.InitializeBullets(bulletNode);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.FireRate += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        private void InitializeBullets(Node bulletNode)
        {
            for (int i = 0; i < 100; i++)
            {
                Bullet bullet = new Bullet();
                this.Bullets.Add(bullet);
                bulletNode.addChild(bullet);
            }
        }

        public void Fire(Vector2 position, Facing facing)
        {
            if (this.FireRate < this.WeaponType.FireRate)
                return;

            this.FireRate = 0;

            foreach (Bullet bullet in Bullets)
            {
                if (bullet.Dead)
                {
                    bullet.Fire(position, facing);
                    return;
                }
            }
        }
    }
}
