using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Microsoft.Xna.Framework;
using CallOfHonour;
using Flakcore;
using Display.Tilemap;
using CallOfHonour.GameObjects;
using Flakcore.Display.ParticleEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Flakcore.Display.ParticleEngine.Modifiers;

namespace Bunker_Hunter.States
{
    class PlayState : State
    {
        ParticleEngine Engine;
        public PlayState()
        {
            this.BackgroundColor = Color.SandyBrown;

            Tilemap tilemap = new Tilemap();
            tilemap.loadMap(@"Content/map2.tmx", 32, 32);
            this.addChild(tilemap);

            Player player = new Player();
            this.addChild(player);

            GameManager.currentDrawCamera.followNode = player;

            // effect test
            ParticleEffect effect = new ParticleEffect();

            effect.BaseTexture = GameManager.content.Load<Texture2D>("LensFlare");
            effect.Modifiers.Add(new RotationRate(5f));
            effect.Modifiers.Add(new LinearAlpha(0));

            this.Engine = new ParticleEngine(effect);
            this.Engine.position = new Vector2(400, 400);
            this.addChild(this.Engine);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.Engine.position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }
    }
}
