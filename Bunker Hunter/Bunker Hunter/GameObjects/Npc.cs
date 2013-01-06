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
        public Npc(Layer bulletLayer)
            : base(bulletLayer, CharacterTypes.ENEMY)
        {
            this.LoadTexture(GameManager.Content.Load<Texture2D>("player"), 32, 48);
            this.AddCollisionGroup("npc");
        }
    }
}
