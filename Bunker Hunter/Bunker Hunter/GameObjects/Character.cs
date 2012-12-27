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
        protected Weapon Weapon;
        protected Node BulletNode;
        protected bool JumpAvailable;

        public Character(Node bulletNode)
        {
            this.BulletNode = bulletNode;
            this.JumpAvailable = false;
            this.Weapon = new Weapon(bulletNode);

            this.addChild(this.Weapon);

            //this.AddAnimation("run", new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 }, 0.08f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Velocity.Y += 10;

            updateAnimations();

            GameManager.collide(this, "tilemap", tilemapCollide);
        }

        private void updateAnimations()
        {
            if (Velocity.X != 0 && Velocity.Y == 0)
                this.PlayAnimation("run");
            else if (Velocity.X == 0 && Velocity.Y == 0)
                this.PlayAnimation("still");
        }

        private bool tilemapCollide(Node player, Node tilemap)
        {
            if (this.Touching.Bottom)
                JumpAvailable = true;

            return true;
        }

        public void takeDamage(Bullet bullet)
        {
            // ouch!
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
}
