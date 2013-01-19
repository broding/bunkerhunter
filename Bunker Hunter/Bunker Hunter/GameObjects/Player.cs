using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Flakcore;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Flakcore.Utils;
using Flakcore.Physics;
using CallOfHonour;
using Bunker_Hunter.Models;

namespace Bunker_Hunker.GameObjects
{
    public class Player : Character
    {
        private Weapon Weapon;
        private Node BulletLayer;

        public Player(Layer bulletLayer) : base(CharacterType.PLAYER)
        {
            this.BulletLayer = bulletLayer;
            this.Weapon = new Weapon(bulletLayer);

            this.LoadTexture("player");

            this.AddChild(this.Weapon);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.UpdateInput(GameManager.Input.GetInputState(PlayerIndex.One));
        }

        private void UpdateInput(InputState inputState)
        {
            this.Run(inputState.X);

            this.Climb(inputState.Y, inputState.Jump);

            if (inputState.Jump)
                Jump();

            if (inputState.Fire)
                Fire();
        }

        private void Fire()
        {
            if (this.Weapon != null)
            {
                this.Weapon.Fire(this.Position, this.Facing, this);
            }
        }
    }
}
