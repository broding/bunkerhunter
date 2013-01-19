using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Flakcore;
using Flakcore.Utils;
using Bunker_Hunter.Models;

namespace Bunker_Hunter.Types
{
    class EnemyType : ITypePool<Enemy>
    {
        public int PoolSize;
        public Texture2D Texture;
        public EnemyBehaviorType BehaviorType { get; private set; }

        public Pool<Enemy> Pool { get; set; }

        public EnemyType()
        {
            this.PoolSize = 20;
            this.Texture = GameManager.Content.Load<Texture2D>("enemy");
            this.BehaviorType = new EnemyBehaviorType();

            this.InitializePool();
        }

        public void InitializePool()
        {
            this.Pool = new Pool<Enemy>(this.PoolSize, false, this.IsEnemyActive, this.SetupEnemy);
        }

        private bool IsEnemyActive(Enemy enemy)
        {
            return !enemy.Active;
        }

        private Enemy SetupEnemy()
        {
            Enemy enemy = new Enemy(this);
            enemy.Deactivate();

            return enemy;
        }
    }
}
