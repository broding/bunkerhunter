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

        public InputState GetInputState(PlayerIndex player)
        {
            InputState state = new InputState();

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
                state.X = -1;
            else if(keyboardState.IsKeyDown(Keys.Right))
                state.X = 1;

            if (keyboardState.IsKeyDown(Keys.Up))
                state.Y = -1;
            else if (keyboardState.IsKeyDown(Keys.Down))
                state.Y = 1;

            if (keyboardState.IsKeyDown(Keys.Space))
                state.Jump = true;

            if (keyboardState.IsKeyDown(Keys.X))
                state.Fire = true;

            return state;
        }
    }

    public struct InputState
    {
        public float X;
        public float Y;
        public bool Jump;
        public bool Fire;
    }
}
