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
using Bunker_Hunter.GameObjects;

namespace Bunker_Hunker.GameObjects
{
    public class Player : Character
    {
        public Player(Layer bulletLayer)
            : base(bulletLayer, CharacterTypes.PLAYER)
        {
            this.LoadTexture(GameManager.Content.Load<Texture2D>("player"), 32, 48);
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
            InputState state = GameManager.Input.GetInputState(PlayerIndex.One);

            if (state.Jump)
                jump();

            if (state.Fire)
                Fire();

            this.Velocity.X = state.X * 166;
        }

        private void Fire()
        {
            if (this.Weapon != null)
            {
                this.Weapon.Fire(this.Position, this.Facing, this);
            }
        }

        private void npcCollide(Node player, Node tilemap)
        {
        }

        protected override void DrawCall(SpriteBatch spriteBatch, Vector2 position, Vector2 scale, float rotation, SpriteEffects spriteEffect)
        {
            base.DrawCall(spriteBatch, position, scale, rotation, spriteEffect);
        }
    }
}
