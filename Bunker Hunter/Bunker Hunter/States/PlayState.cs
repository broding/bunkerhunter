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
using System.Threading;
using Bunker_Hunter.GameDirector;
using Bunker_Hunter.Types;
using Bunker_Hunter.Models;
using Bunker_Hunter.UI;

namespace Bunker_Hunter.States
{
    class PlayState : State
    {
        private Director Director;
        private Player Player;
        private Level Level;

        public PlayState()
        {
            this.Load();
        }

        public override void Load()
        {
            GameManager.LevelBorderSize = new Vector2(250, 250);
            GameManager.BulletLayer = new Layer();
            GameManager.UILayer = new Layer();

            DamageIndicator.Initialize(GameManager.UILayer);

            this.Level = new Level();
            this.Player = new Player(GameManager.BulletLayer);
            this.Player.Position = new Vector2(32, 0);
            this.Director = new Director(this.Player, this.Level, GameManager.BulletLayer);

            this.AddChild(this.Level);
            this.AddChild(Player);
            this.AddChild(GameManager.BulletLayer);
            this.AddChild(GameManager.UILayer);

            GameManager.currentDrawCamera.followNode = this.Player;

            this.SpawnPlayer();

            Enemy.Players = new List<Player>(4);
            Enemy.Players.Add(this.Player);

            EnemyType enemyType = new EnemyType();
            Enemy enemy = enemyType.Pool.New();
            enemy.Activate();
            enemy.Position = this.Player.Position - new Vector2(64, 64);
            this.AddChild(enemy);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GameManager.collide(this.Player, "end", null, this.PlayerEndCollision);
        }

        private bool PlayerEndCollision(Node player, Node end)
        {
            if (GameManager.Input.GetInputState(PlayerIndex.One).Y < -0.5)
            {
                this.Load();
            }

            return false;
        }

        private void SpawnPlayer()
        {
            this.Player.Position = this.Level.StartPosition + new Vector2(48, 48);
        }
    }
}
