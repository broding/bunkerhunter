using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bunker_Hunker.GameObjects;
using Flakcore.Display;
using Bunker_Hunter.Models;
using Bunker_Hunter.Types;

namespace Bunker_Hunter.Models.Enemies
{
    class Enemy : Sprite
    {
        public EnemyType EnemyType { get; private set; }

        public Enemy(Layer bulletLayer) : base()
        {
            this.LoadTexture("enemy");
        }
    }
}
