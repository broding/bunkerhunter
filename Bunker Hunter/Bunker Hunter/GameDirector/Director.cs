using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bunker_Hunker.GameObjects;
using Flakcore.Display.Level;
using Bunker_Hunter.Models.Enemies;
using Flakcore.Display;

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

            this.LoadEnemies();
        }

        private void LoadEnemies()
        {
            this.Enemies = new Enemy[50];

            for (int i = 0; i < 50; i++)
            {
                this.Enemies[i] = new Enemy(this.BulletLayer);
                this.Enemies[i].Kill();
            }
        }
    }
}
