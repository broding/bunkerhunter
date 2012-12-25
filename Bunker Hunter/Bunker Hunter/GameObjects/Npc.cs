using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore;
using Microsoft.Xna.Framework.Graphics;

namespace CallOfHonour.GameObjects
{
    class Npc : Character
    {
        public Npc() : base()
        {
            this.LoadTexture(GameManager.content.Load<Texture2D>("npc"), 32, 64);
            this.addCollisionGroup("npc");
        }
    }
}
