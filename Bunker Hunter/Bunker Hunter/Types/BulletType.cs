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
    public class BulletType : ITypePool
    {
        public int PoolSize { get; private set; }
        public string TextureName { get; private set; }
        public Vector2 Speed { get; private set; }
        public Vector2 SpeedChange { get; private set; }
        public float Mass { get; private set; }

        public Pool<Bullet> Pool;

        public BulletType()
        {
            this.PoolSize = 30;
            this.TextureName = "bullet";
            this.Speed = new Vector2(800, -100);
            this.SpeedChange = new Vector2(1.00f, 0f);
            this.Mass = 0.0f;

            this.InitializePool();
        }

        public void InitializePool()
        {
            this.Pool = new Pool<Bullet>(this.PoolSize, false, this.IsBulletDead, this.SetupBullet);
        }

        private bool IsBulletDead(Bullet bullet)
        {
            return bullet.Dead;
        }

        private Bullet SetupBullet()
        {
            Bullet bullet = new Bullet(GameManager.BulletLayer, this);
            bullet.Kill();

            return bullet;
        }
    }
}
