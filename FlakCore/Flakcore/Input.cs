using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Flakcore
{
    public class Input
    {
        private GamePadState[] previousStates = new GamePadState[4];

        public void update()
        {
            this.previousStates[(int)PlayerIndex.One] = GamePad.GetState(PlayerIndex.One);
            this.previousStates[(int)PlayerIndex.Two] = GamePad.GetState(PlayerIndex.Two);
            this.previousStates[(int)PlayerIndex.Three] = GamePad.GetState(PlayerIndex.Three);
            this.previousStates[(int)PlayerIndex.Four] = GamePad.GetState(PlayerIndex.Four);
        }

        public GamePadState getPadState(PlayerIndex player)
        {
            return GamePad.GetState(player);
        }

        public bool justPressed(PlayerIndex player, Buttons button)
        {
            GamePadState currentState = GamePad.GetState(player);

            if(currentState.IsButtonDown(button) && this.previousStates[(int)player].IsButtonUp(button))
                return true;
            else
                return false;
        }
    }
}
