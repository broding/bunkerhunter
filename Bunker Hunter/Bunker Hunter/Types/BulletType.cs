using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CallOfHonour.GameObjects;
using Flakcore.Utils;
using Flakcore.Display;
using Flakcore;

namespace Bunker_Hunter.Types
{
    public class BulletType : ITypePool<Bullet>
    {
        public int PoolSize { get; private set; }
        public string TextureName { get; private set; }
        public Vector2 Speed { get; private set; }
        public Vector2 SpeedChange { get; private set; }
        public float Mass { get; private set; }

        public Pool<Bullet> Pool { get; set; }

        public BulletType()
        {
            this.PoolSize = 30;
            this.TextureName = "bullet";
            this.Speed = new Vector2(1000, -100);
            this.SpeedChange = new Vector2(1.00f, 0f);
            this.Mass = 0.0f;

            this.InitializePool();
        }

        public void InitializePool()
        {
            this.Pool = new Pool<Bullet>(this.PoolSize, false, this.IsBulletActive, this.SetupBullet);
        }

        private bool IsBulletActive(Bullet bullet)
        {
            return !bullet.Active;
        }

        private Bullet SetupBullet()
        {
            Bullet bullet = new Bullet(GameManager.BulletLayer, this);
            bullet.Deactivate();

            return bullet;
        }
    }
}
