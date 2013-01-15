using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Microsoft.Xna.Framework;
using Flakcore;
using Microsoft.Xna.Framework.Input;

namespace Bunker_Hunter.States
{
    class MenuState : State
    {
        public MenuState()
        {
            this.BackgroundColor = Color.DarkRed;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GameManager.Input.JustPressed(PlayerIndex.One, Keys.Space))
            {
                GameManager.SwitchState(typeof(PlayState), StateTransition.FADE, StateTransition.FADE);
            }
        }
    }
}
