using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Flakcore.Display.ParticleEngine
{
    public class ParticleEngine : Node
    {
        public Vector2 EmitterPosition = Vector2.Zero;
        private ParticleEffect Effect;
        private List<Particle> Particles;
        private List<Particle> DeadParticles;

        private int ReleaseTimer;

        public ParticleEngine(ParticleEffect Effect)
        {
            this.Effect = Effect;
            this.Particles = new List<Particle>();
            this.DeadParticles = new List<Particle>();
            this.ReleaseTimer = 0;

            this.InitParticles();
        }

        private void InitParticles()
        {
            int amount = this.Effect.TotalParticles;

            for (int i = 0; i < amount; i++)
            {
                Particle particle = new Particle(this.KillParticle, this.Effect);
                this.DeadParticles.Add(particle);
                this.addChild(particle);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.ReleaseTimer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.ReleaseTimer > this.Effect.ReleaseSpeed)
            {
                this.ReleaseTimer = 0;
                this.Emit(this.Effect.ReleaseQuantity);
            }
        }

        private void Emit(int quanitity)
        {
            if (this.DeadParticles.Count < quanitity)
                quanitity = this.DeadParticles.Count;

            for (int i = 0; i < quanitity; i++)
            {
                int lastIndex = this.DeadParticles.Count-1;
                Particle particle = this.DeadParticles[lastIndex];
                particle.Fire(this.getWorldPosition());
                this.Particles.Add(particle);
                this.DeadParticles.RemoveAt(lastIndex);
            }
        }

        internal void KillParticle(Particle particle)
        {
            this.Particles.Remove(particle);
            particle.Kill();
            this.DeadParticles.Add(particle);
        }
    }
}
