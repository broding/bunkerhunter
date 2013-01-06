using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Microsoft.Xna.Framework;
using CallOfHonour;
using Flakcore;
using CallOfHonour.GameObjects;
using Flakcore.Display.ParticleEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Flakcore.Display.ParticleEngine.Modifiers;
using Bunker_Hunker.GameObjects;
using Display.Tilemap;
using Flakcore.Display.Level;

namespace Bunker_Hunter.States
{
    class PlayState : State
    {
        public PlayState()
        {
            this.BackgroundColor = Color.SandyBrown;

            Layer bulletLayer = new Layer();
            //bulletLayer.Depth = 0.3f;

            Level level = new Level();
            this.AddChild(level);

            Player player = new Player(bulletLayer);
            player.Position = new Vector2(32, 0);

            //Npc npc1 = new Npc(bulletLayer);
           // npc1.Position = new Vector2(380, 160);
            //this.AddChild(npc1);

            GameManager.currentDrawCamera.followNode = player;

            // effect test

            this.AddChild(player);
            this.AddChild(bulletLayer);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
