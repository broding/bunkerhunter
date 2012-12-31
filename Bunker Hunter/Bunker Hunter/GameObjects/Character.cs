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
using Bunker_Hunter.GameObjects;

namespace Bunker_Hunker.GameObjects
{
    public abstract class Character : Sprite
    {
        public CharacterTypes Type { get; private set; }
        public int Health { get; protected set; }

        protected Weapon Weapon;
        protected Node BulletLayer;

        protected bool JumpAvailable;

        protected bool LadderAvailable;
        protected Node LadderTile;
        protected bool LadderClimbing;

        public Character(Layer bulletLayer, CharacterTypes type)
        {
            this.Type = type;
            this.Health = 100;
            this.Mass = 1;
            this.BulletLayer = bulletLayer;

            this.JumpAvailable = false;
            this.LadderAvailable = false;
            this.LadderClimbing = false;

            this.Weapon = new Weapon(bulletLayer);
            this.addCollisionGroup("character");

            this.AddChild(this.Weapon);

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

        private void TilemapCollide(Node player, Node tilemap)
        {
            if (this.Touching.Bottom)
            {
                this.JumpAvailable = true;
                this.LadderClimbing = false;
            }
        }

        private bool LadderOverlap(Node player, Node tile)
        {
            if (Math.Abs(tile.Position.X - player.Position.X) < 10)
            {
                this.LadderTile = tile;
                this.LadderAvailable = true;
            }

            return false;
        }

        public void Hit(Bullet bullet)
        {
            this.Health -= 4;
        }

        protected void UpdateInput(InputState inputState)
        {
            this.Run(inputState.X);

            this.Climb(inputState.Y, inputState.Jump);

            if (inputState.Jump)
                Jump();

            if (inputState.Fire)
                Fire();
        }
            
        protected void Run(float speed)
        {
            this.Velocity.X = speed * 166;
        }

        protected void Climb(float ySpeed, bool exitButton)
        {
            if (this.LadderAvailable)
            {
                if ((ySpeed != 0 || this.LadderClimbing) && !exitButton)
                {
                    this.Position.X = this.LadderTile.Position.X;
                    this.Velocity.X = 0;
                    this.Velocity.Y = ySpeed * 166;
                    this.LadderClimbing = true;
                }
                else
                {
                    this.LadderClimbing = false;
                }
            }
        }

        protected void Jump()
        {
            if (this.JumpAvailable) 
            {
                this.PlayAnimation("airing");
                this.Velocity.Y = -220;
                this.JumpAvailable = false;
            }
        }

        private void Fire()
        {
            if (this.Weapon != null)
            {
                this.Weapon.Fire(this.Position, this.Facing, this);
            }
        }
    }

    public enum CharacterTypes
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
