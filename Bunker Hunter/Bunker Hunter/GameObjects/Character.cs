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
        public int Health { get; private set; }
        protected Weapon Weapon;
        protected Node BulletLayer;
        protected bool JumpAvailable;

        public Character(Layer bulletLayer, CharacterTypes type)
        {
            this.Type = type;
            this.Health = 100;
            this.Mass = 1;
            this.BulletLayer = bulletLayer;
            this.JumpAvailable = false;
            this.Weapon = new Weapon(bulletLayer);
            this.addCollisionGroup("character");

            this.addChild(this.Weapon);

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

            updateAnimations();
            this.UpdateFacing();

            GameManager.collide(this, "tilemap", tilemapCollide);
        }

        private void UpdateFacing()
        {
            if (this.Velocity.X > 0)
                this.Facing = Facing.Right;
            else if (this.Velocity.X < 0)
                this.Facing = Facing.Left;
        }

        private void updateAnimations()
        {
            if (Velocity.X != 0 && Velocity.Y == 0)
                this.PlayAnimation("run");
            else if (Velocity.X == 0 && Velocity.Y == 0)
                this.PlayAnimation("still");
        }

        private void tilemapCollide(Node player, Node tilemap)
        {
            if (this.Touching.Bottom)
                JumpAvailable = true;
        }

        public void Hit(Bullet bullet)
        {
            this.Health -= 4;
        }

        protected void shoot()
        {
            //if (facing == Facing.Left)
                //BulletManager.activateBullet(new Vector2(body.Position.X - ConvertUnits.ToSimUnits(width / 2), body.Position.Y + ConvertUnits.ToSimUnits(10)), new Vector2(-1, 0));
            //else
                //BulletManager.activateBullet(new Vector2(body.Position.X + ConvertUnits.ToSimUnits(width / 2), body.Position.Y + ConvertUnits.ToSimUnits(10)), new Vector2(1, 0));
        }

        protected void run(int speed)
        {
            if (speed > 0)
                Facing = Facing.Right;
            else if (speed < 0)
                Facing = Facing.Left;

            this.Velocity.X = speed;
        }

        protected void jump()
        {
            if (this.JumpAvailable) 
            {
                this.PlayAnimation("airing");
                this.Velocity.Y = -220;
                this.JumpAvailable = false;
            }
        }

        protected void stop()
        {
            this.Velocity.X = 0;
        }
    }

    public enum CharacterTypes
    {
        PLAYER,
        ENEMY
    }
}
