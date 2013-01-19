using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Flakcore;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Flakcore.Utils;
using CallOfHonour.GameObjects;
using Flakcore.Physics;
using Bunker_Hunter.Models;
using Bunker_Hunter.UI;

namespace Bunker_Hunker.GameObjects
{
    public abstract class Character : Sprite
    {
        public CharacterType Type { get; private set; }
        public int Health { get; protected set; }

        protected bool JumpAvailable;

        protected bool LadderAvailable;
        protected Node LadderTile;
        protected bool LadderClimbing;

        public Character(CharacterType type)
        {
            this.Type = type;
            this.Health = 100;
            this.Mass = 1.3f;
            this.Depth = 0.1f;
            this.Collidable = true;

            this.JumpAvailable = false;
            this.LadderAvailable = false;
            this.LadderClimbing = false;

            this.AddCollisionGroup("character");

            //this.AddAnimation("run", new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 }, 0.08f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Health <= 0)
            {
                this.Collidable = false;
                // play dead animation
            }
            
            this.UpdateAnimations();
            this.UpdateFacing();

            GameManager.collide(this, "tilemap", TilemapCollide);
            GameManager.collide(this, "ladder", null, LadderOverlap);
            GameManager.collide(this, "ladderArea", LadderAreaCollide, LadderAreaCheck);
        }

        public override void PreCollisionUpdate(GameTime gameTime)
        {
            base.PreCollisionUpdate(gameTime);

            this.LadderAvailable = false;
        }

        private void UpdateFacing()
        {
            if (this.Velocity.X > 0)
                this.Facing = Facing.Right;
            else if (this.Velocity.X < 0)
                this.Facing = Facing.Left;
        }

        private void UpdateAnimations()
        {
            if (Velocity.X != 0 && Velocity.Y == 0)
                this.PlayAnimation("run");
            else if (Velocity.X == 0 && Velocity.Y == 0)
                this.PlayAnimation("still");
        }

        protected virtual void TilemapCollide(Node player, Node tilemap)
        {
            if (this.Touching.Bottom)
            {
                this.JumpAvailable = true;
                this.LadderClimbing = false;
            }
        }

        private bool LadderOverlap(Node player, Node tile)
        {
            if (Math.Abs(tile.WorldPosition.X - player.WorldPosition.X) < 10)
            {
                this.LadderTile = tile;
                this.LadderAvailable = true;
            }

            return false;
        }

        public bool LadderAreaCheck(Node character, Node ladderArea)
        {
            if (this.LadderClimbing || ((this.WorldPosition.Y + this.Height/1.2) >= ladderArea.WorldPosition.Y && this.Velocity.Y > 0))
                return false;

            return true;
        }

        private void LadderAreaCollide(Node player, Node tilemap)
        {
            this.TilemapCollide(player, tilemap);
        }

        public virtual void Hit(Bullet bullet)
        {
            this.Health -= 4;
            DamageIndicator.Show(this, 126);
        }
            
        protected void Run(float speed)
        {
            this.Velocity.X = speed * 326;
        }

        protected void Climb(float ySpeed, bool exitButton)
        {
            if (this.LadderAvailable)
            {
                if ((ySpeed != 0 || this.LadderClimbing) && !exitButton)
                {
                    this.Position.X = this.LadderTile.WorldPosition.X;
                    this.Velocity.X = 0;
                    this.Velocity.Y = ySpeed * 166;
                    this.LadderClimbing = true;
                }
                else
                {
                    this.LadderClimbing = false;
                }
            }
            else
            {
                this.LadderClimbing = false;
            }
        }

        protected void Jump()
        {
            if (this.JumpAvailable) 
            {
                this.PlayAnimation("airing");
                this.Velocity.Y = -520;
                this.JumpAvailable = false;
            }
        }

        public bool Dead
        {
            get
            {
                return this.Health <= 0;
            }
        }
    }

    public enum CharacterType
    {
        PLAYER,
        ENEMY
    }

    public enum CharacterState
    {
        JUMPING,
        CLIMBING,
        WALKING
    }
}
