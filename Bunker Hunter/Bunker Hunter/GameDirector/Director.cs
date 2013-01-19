using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bunker_Hunker.GameObjects;
using Flakcore.Display.Level;
using Flakcore.Display;
using Bunker_Hunter.Models;

namespace Bunker_Hunter.GameDirector
{
    class Director
    {
        private Player Player;
        private Level Level;
        private Enemy[] Enemies;
        private Layer BulletLayer;

        public Director(Player player, Level level, Layer bulletLayer)
        {
            this.Player = player;
            this.Level = level;
            this.BulletLayer = bulletLayer;
        }
    }
}
