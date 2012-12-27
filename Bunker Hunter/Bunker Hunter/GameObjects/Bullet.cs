using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Flakcore;
using Microsoft.Xna.Framework;
using Flakcore.Utils;
using Bunker_Hunter.GameObjects;

namespace CallOfHonour.GameObjects
{
    public class Bullet : Sprite
    {
        public BulletType BulletType { get; private set; }

        public Bullet()
        {
            this.LoadTexture("bullet");
            this.addCollisionGroup("bullet");
            this.BulletType = new BulletType();
            this.Kill();
        }

        public void Fire(Vector2 position, Facing facing)
        {
            this.Position = position;
            this.Facing = facing;
            this.Revive();
            this.Velocity.X = Util.FacingToVelocity(facing) * this.BulletType.Speed;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GameManager.collide(this, "tilemap", this.TilemapCollision);
        }

        private bool TilemapCollision(Node bullet, Node tilemap)
        {
            return true;
        }
    }
}
