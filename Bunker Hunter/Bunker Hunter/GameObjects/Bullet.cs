using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Flakcore;
using Microsoft.Xna.Framework;
using Flakcore.Utils;
using Bunker_Hunter.GameObjects;
using Flakcore.Display.ParticleEngine;
using Microsoft.Xna.Framework.Graphics;
using Flakcore.Display.ParticleEngine.Modifiers;
using Bunker_Hunker.GameObjects;

namespace CallOfHonour.GameObjects
{
    public class Bullet : Sprite
    {
        public BulletType BulletType { get; private set; }
        public Character Shooter { get; private set; }
        public ParticleEngine ParticleEngine;
        private ParticleEngine ExplosionParticles;

        public Bullet(Layer bulletLayer, BulletType bulletType)
        {
            this.Collidable = true;
            this.BulletType = bulletType;

            ParticleEffect effect = GameManager.Content.Load<ParticleEffect>(@"ParticleEffects/smoke1");
            this.ParticleEngine = new ParticleEngine(effect);
            bulletLayer.AddChild(this.ParticleEngine);

            ParticleEffect effect2 = GameManager.Content.Load<ParticleEffect>(@"ParticleEffects/smokePuff");
            this.ExplosionParticles = new ParticleEngine(effect2);
            bulletLayer.AddChild(this.ExplosionParticles);

            this.LoadTexture(this.BulletType.TextureName);
            this.AddCollisionGroup("bullet");
            this.Kill();
        }

        public void Fire(Vector2 position, Facing facing, Character shooter)
        {
            this.Position = position;
            this.Facing = facing;
            this.Shooter = shooter;
            this.Revive();
            this.ParticleEngine.Position = this.Position;
            this.ParticleEngine.Start();

            this.Velocity.X = Util.FacingToVelocity(facing) * this.BulletType.Speed.X;
            this.Velocity.Y = this.BulletType.Speed.Y;
            this.Mass = this.BulletType.Mass;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Dead)
                return;

            this.Velocity *= this.BulletType.SpeedChange;
            this.ParticleEngine.EmitterPosition = this.Position;

            GameManager.collide(this, "tilemap", this.TilemapCollision);
            GameManager.collide(this, "character", this.CharacterCollision);
        }

        private void TilemapCollision(Node bullet, Node tile)
        {
            if (this.Facing == Facing.Left)
                this.ExplosionParticles.EmitterPosition = new Vector2(tile.WorldPosition.X + tile.Width, this.WorldPosition.Y);
            else
                this.ExplosionParticles.EmitterPosition = new Vector2(tile.WorldPosition.X, this.WorldPosition.Y);

            this.ExplosionParticles.Explode();

            this.Kill();
            this.ParticleEngine.Stop();
        }

        private void CharacterCollision(Node bullet, Node characterNode)
        {
            Character character = (Character)characterNode;

            if (this.Shooter.Type == CharacterTypes.PLAYER && character.Type == CharacterTypes.ENEMY)
            {
                character.Hit(this);
                this.Kill();
            }
            else if (this.Shooter.Type == CharacterTypes.ENEMY && character.Type == CharacterTypes.PLAYER)
            {
                character.Hit(this);
                this.Kill();
            }
        }
    }
}
