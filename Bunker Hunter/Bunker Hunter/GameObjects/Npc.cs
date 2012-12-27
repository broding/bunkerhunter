using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore;
using Microsoft.Xna.Framework.Graphics;
using Flakcore.Display;

namespace Bunker_Hunker.GameObjects
{
    class Npc : Character
    {
        public Npc(Node bulletNode) : base(bulletNode)
        {
            this.LoadTexture(GameManager.content.Load<Texture2D>("npc"), 32, 48);
            this.addCollisionGroup("npc");
        }
    }
}
