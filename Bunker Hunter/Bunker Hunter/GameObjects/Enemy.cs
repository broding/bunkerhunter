using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bunker_Hunker.GameObjects;
using Flakcore.Display;
using Bunker_Hunter.Models;
using Bunker_Hunter.Types;
using Flakcore.Utils;
using Flakcore;
using Microsoft.Xna.Framework;
using CallOfHonour.GameObjects;

namespace Bunker_Hunter.Models
{
    class Enemy : Character, IPoolable
    {
        public static List<Player> Players;

        public EnemyType EnemyType { get; private set; }

        public int PoolIndex { get; set; }
        public Action<int> ReportDeadToPool { get; set; }

        private bool Aggresive;
        private Player TargetPlayer;
        private int BehaviorUpdateTimer;
        private int JumpTimer;

        public Enemy(EnemyType type) : base(CharacterType.ENEMY)
        {
            this.LoadTexture("enemy");

            this.EnemyType = type;
            this.Collidable = true;
            this.Mass = 1;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
    
            this.BehaviorUpdateTimer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.BehaviorUpdateTimer > this.EnemyType.BehaviorType.BehaviorUpdate && !this.Dead)
            {
                this.BehaviorUpdateTimer = 0;
                this.SelectTarget();
                this.UpdateBehavior(gameTime);
            }
            else if(this.Dead)
            {
                this.Velocity = Vector2.Zero;
            }
        }

        private void UpdateBehavior(GameTime gameTime)
        {
            this.HandleRun();
            this.HandleJump(gameTime);
        }

        private void HandleRun()
        {
            if (this.EnemyType.BehaviorType.Runs && this.TargetPlayer != null)
            {
                this.Velocity.X = this.EnemyType.BehaviorType.RunSpeed * Util.FacingToVelocity(this.Facing);
            }
        }

        private void HandleJump(GameTime gameTime)
        {
            if (this.EnemyType.BehaviorType.Jumps && this.TargetPlayer != null)
            {
                this.JumpTimer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (this.JumpTimer > this.EnemyType.BehaviorType.JumpInterval)
                {
                    this.Jump();
                    this.JumpTimer = 0;
                }
            }
        }

        private void SelectTarget()
        {
            this.TargetPlayer = Enemy.Players[0];

            if (this.EnemyType.BehaviorType.Aggresive || this.Aggresive)
            {
                if (this.Position.X > this.TargetPlayer.Position.X)
                    this.Facing = Facing.Left;
                else
                    this.Facing = Facing.Right;
            }
        }

        public override void Hit(Bullet bullet)
        {
            base.Hit(bullet);

            this.Aggresive = true;
            this.SelectTarget();
            this.HandleRun();
        }

        protected override void TilemapCollide(Node player, Node tilemap)
        {
            base.TilemapCollide(player, tilemap);

            if (this.Touching.Left || this.Touching.Right)
            {
                if (this.Facing == Facing.Left)
                    this.Facing = Facing.Right;
                else
                    this.Facing = Facing.Left;

                this.HandleRun();
                this.BehaviorUpdateTimer = 0;
            }
        }
    }
}
