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
        public Player(Layer bulletLayer)
            : base(bulletLayer, CharacterType.PLAYER)
        {
            this.LoadTexture("player");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.UpdateInput(GameManager.Input.GetInputState(PlayerIndex.One));
        }
    }
}
