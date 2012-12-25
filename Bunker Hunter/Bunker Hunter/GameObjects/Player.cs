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
namespace CallOfHonour
{
    public class Player : Character
    {
        public Player() : base()
        {
            this.LoadTexture(GameManager.content.Load<Texture2D>("poppetje"), 16, 32);
        }

        public override void Update(GameTime gameTime)
        {
            updateInput();

            GameManager.collide(this, "npc", npcCollide);

            base.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            base.PostUpdate(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix parentTransform)
        {
            base.Draw(spriteBatch, parentTransform);
        }

        private void updateInput()
        {
            GamePadState padState = GamePad.GetState(PlayerIndex.One);

            if (GameManager.input.justPressed(PlayerIndex.One, Buttons.A))
                jump();
            else if (padState.ThumbSticks.Left.X != 0)
                run((int)(padState.ThumbSticks.Left.X * 100));
            else
                stop();

            if (GameManager.input.justPressed(PlayerIndex.One, Buttons.B))
                shoot();

            this.Velocity.X = padState.ThumbSticks.Left.X * 100;
        }

        private bool npcCollide(Node player, Node tilemap)
        {
            return true;
        }
    }
}
