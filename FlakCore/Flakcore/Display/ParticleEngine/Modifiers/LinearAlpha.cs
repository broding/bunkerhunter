using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RC_Management_Game.Activities;

namespace Flakcore.Display.ParticleEngine.Modifiers
{
    public class LinearAlpha : IParticleModifier
    {
        private Particle Target;
        private float FinalAlpha;

        private float Time;

        public LinearAlpha(float finalAlpha)
        {
            this.FinalAlpha = finalAlpha;
        }

        public void SetParticle(Particle particle)
        {
            this.Target = particle;
        }

        public void Apply()
        {
            this.Time = 0;
            this.Target.Alpha = this.Target.Effect.ReleaseAlpha;
        }

        public void Update(GameTime gameTime)
        {
            this.Time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            this.Target.Alpha = Easing.Linear(
                this.Time,
                this.Target.Effect.ReleaseAlpha,
                this.FinalAlpha - this.Target.Effect.ReleaseAlpha,
                this.Target.Effect.Lifetime
                );
        }

        public object Clone()
        {
            return new LinearAlpha(this.FinalAlpha);
        }
    }
}
