using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Flakcore.Utils;

namespace Flakcore.Display.ParticleEngine
{
    public class Particle : Sprite
    {
        public ParticleEffect Effect { get; private set; }

        private Action<Particle> KillCallBack;
        private int Lifetime;
        private LinkedList<IParticleModifier> Modifiers;

        public Particle(Action<Particle> killCallBack, ParticleEffect effect) : base()
        {
            this.KillCallBack = killCallBack;
            this.Effect = effect;
            this.Lifetime = 0;
            this.Modifiers = new LinkedList<IParticleModifier>();
            this.Kill();
            this.Origin = new Vector2(this.Effect.BaseTexture.Width / 2, this.Effect.BaseTexture.Height / 2);

            this.InitializeModifiers();
        }

        private void InitializeModifiers()
        {
            foreach (IParticleModifier modifier in this.Effect.Modifiers)
            {
                IParticleModifier addedModifier = this.Modifiers.AddLast((IParticleModifier)modifier.Clone()).Value;
                addedModifier.SetParticle(this);
            }
        }

        private void InitializeEffect()
        {
            Random random = new Random();

            this.Lifetime = 0;
            this.Velocity = new Vector2(
                (this.Effect.ReleaseVelocity.X + this.Effect.ReleaseVelocityVariantion.X * Util.RandomPositiveNegative()) * (float)Math.Cos(random.NextDouble() * (Math.PI * 2)),
                (this.Effect.ReleaseVelocity.Y + this.Effect.ReleaseVelocityVariantion.Y * Util.RandomPositiveNegative()) * (float)Math.Sin(random.NextDouble() * (Math.PI * 2)));
            this.Scale = this.Effect.ReleaseScale + this.Effect.ReleaseScaleVariation * Util.RandomPositiveNegative();
            this.Color = this.Effect.ReleaseColor;
            this.Rotation = this.Effect.ReleaseRotation + this.Effect.ReleaseRotationVariation * Util.RandomPositiveNegative();

            foreach (IParticleModifier modifier in this.Modifiers)
            {
                modifier.Apply();
            }
        }

        public void Fire(Vector2 position)
        {
            this.Revive();
            this.Position = position;
            this.InitializeEffect();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.Lifetime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Lifetime > this.Effect.Lifetime)
            {
                this.Lifetime = 0;
                this.KillCallBack(this);
            }

            foreach (IParticleModifier modifier in this.Modifiers)
                modifier.Update(gameTime);
        }

        protected override void DrawCall(SpriteBatch spriteBatch, Vector2 position, Vector2 scale, float rotation, SpriteEffects spriteEffect)
        {
            spriteBatch.Draw(
                this.Effect.BaseTexture,
                new Vector2(this.Position.X * ScrollFactor.X, this.Position.Y * ScrollFactor.Y),
                new Rectangle(0, 0, this.Effect.BaseTexture.Width, this.Effect.BaseTexture.Height),
                this.Color * this.Alpha,
                0,
                this.Origin,
                this.Scale,
                spriteEffect,
                1.0f);

        }
    }
}
