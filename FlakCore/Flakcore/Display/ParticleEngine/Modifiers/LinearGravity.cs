using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Flakcore.Display.ParticleEngine.Modifiers
{
    public class LinearGravity : IParticleModifier
    {
        private Particle Target;
        private Vector2 Gravity;

        public LinearGravity(Vector2 gravity)
        {
            this.Gravity = gravity;
        }

        public void SetParticle(Particle particle)
        {
            this.Target = particle;
        }

        public void Apply()
        {
        }

        public void Update(GameTime gameTime)
        {
            this.Target.Velocity += this.Gravity;
        }

        public object Clone()
        {
            return new LinearGravity(this.Gravity);
        }
    }
}
