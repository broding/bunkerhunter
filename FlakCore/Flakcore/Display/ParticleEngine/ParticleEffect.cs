using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Flakcore.Display.ParticleEngine
{
    public class ParticleEffect
    {
        public Texture2D BaseTexture;
        public int TotalParticles = 100;

        public int Lifetime = 2000;

        public int ReleaseQuantity = 1;
        public int ReleaseSpeed = 90;
        public int ReleaseSpeedVariation = 0;

        public Vector2 ReleaseVelocity = Vector2.One * 300;
        public Vector2 ReleaseVelocityVariantion = Vector2.One * 20;

        public Color ReleaseColor = Color.White;
        public Color ReleaseColorVariation = Color.Black;

        public float ReleaseAlpha = 1f;
        public float ReleaseAlphaVariation = 0;

        public float ReleaseRotation = 0;
        public float ReleaseRotationVariation = 1;

        public Vector2 ReleaseScale = Vector2.One;
        public Vector2 ReleaseScaleVariation = Vector2.Zero;

        public List<IParticleModifier> Modifiers;

        public Emitter Emitter;

        public ParticleEffect()
        {
            this.Emitter = new Emitter();
            this.Modifiers = new List<IParticleModifier>();
        }
    }
}
