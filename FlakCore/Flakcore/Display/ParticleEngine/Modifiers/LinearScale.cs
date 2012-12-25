using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RC_Management_Game.Activities;

namespace Flakcore.Display.ParticleEngine.Modifiers
{
    public class LinearScale : IParticleModifier
    {
        private Particle Target;
        private Vector2 FinalScale;

        private float Time;

        public LinearScale(Vector2 finalScale)
        {
            this.FinalScale = finalScale;
        }

        public void SetParticle(Particle particle)
        {
            this.Target = particle;
        }

        public void Apply()
        {
            this.Time = 0;
            this.Target.Scale = this.Target.Effect.ReleaseScale;
        }

        public void Update(GameTime gameTime)
        {
            this.Time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            this.Target.Scale.X = Easing.Linear(
                this.Time,
                this.Target.Effect.ReleaseScale.X,
                this.FinalScale.X - this.Target.Effect.ReleaseScale.X,
                this.Target.Effect.Lifetime
                );

            this.Target.Scale.Y = Easing.Linear(
                this.Time,
                this.Target.Effect.ReleaseScale.Y,
                this.FinalScale.Y - this.Target.Effect.ReleaseScale.Y,
                this.Target.Effect.Lifetime
                );
        }

        public object Clone()
        {
            return new LinearScale(this.FinalScale);
        }
    }
}
