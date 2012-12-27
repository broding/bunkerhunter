using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flakcore.Display
{
    public class Sprite : Node
    {
        public Texture2D Texture { get; protected set; }
        public Facing Facing;
        public Color Color;
        public float Alpha;

        private List<Animation> Animations;
        private bool Animating;
        private double AnimationTimer;
        private int CurrentFrame;
        private Animation CurrentAnimation;

        public Sprite() : base()
        {
            Animations = new List<Animation>();
            Facing = Facing.Right;
            Color = Color.White;
            Alpha = 1;
        }

        public void LoadTexture(string assetName)
        {
            this.LoadTexture(GameManager.content.Load<Texture2D>(assetName));
        }

        public void LoadTexture(Texture2D texture)
        {
            this.LoadTexture(texture, texture.Width, texture.Height);
        }

        public void LoadTexture(Texture2D texture, int width, int height)
        {
            this.Texture = texture;
            this.Width = width;
            this.Height = height;
            this.Animating = false;
        }

        public void AddAnimation(string name, int[] frames, float frameRate)
        {
            Animations.Add(new Animation(name, frames, frameRate));
        }

        public void PlayAnimation(string name)
        {
            // check if the wanted animated is already running
            if (CurrentAnimation.name == name)
                return;

            foreach (Animation animation in Animations)
            {
                if (animation.name == name)
                {
                    CurrentAnimation = animation;
                    Animating = true;
                    AnimationTimer = 0;
                    CurrentFrame = 0;

                    return;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            // if animating, then update all animation stuff
            if (Animating)
            {
                AnimationTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (AnimationTimer > CurrentAnimation.frameRate)
                {
                    if (CurrentFrame == CurrentAnimation.frames.Length-1)
                        CurrentFrame = 0;
                    else
                        CurrentFrame++;

                    AnimationTimer = 0;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix parentTransform)
        {
            if (!Visable)
                return;

            Matrix globalTransform = this.getLocalTransform() * parentTransform * GameManager.currentDrawCamera.getTransformMatrix();

            Vector2 position, scale;
            float rotation;

            Node.decomposeMatrix(ref globalTransform, out position, out rotation, out scale);

            SpriteEffects spriteEffect = new SpriteEffects();

            if(Facing == Facing.Left)
                 spriteEffect = SpriteEffects.FlipHorizontally;

            this.DrawCall(spriteBatch, position, scale, rotation, spriteEffect);

            base.Draw(spriteBatch, parentTransform);   
        }

        protected override void DrawCall(SpriteBatch spriteBatch, Vector2 position, Vector2 scale, float rotation, SpriteEffects spriteEffect)
        {

            if (Animating)
                spriteBatch.Draw(Texture,
                    new Vector2(position.X * ScrollFactor.X, position.Y * ScrollFactor.Y),
                    new Rectangle(CurrentAnimation.frames[CurrentFrame] * Width, 0, Width, Height),
                    this.Color * this.Alpha,
                    rotation,
                    this.Origin,
                    scale,
                    spriteEffect,
                    1.0f);
            else
                spriteBatch.Draw(Texture,
                    new Vector2(position.X * ScrollFactor.X, position.Y * ScrollFactor.Y),
                    new Rectangle(0, 0, Width, Height),
                    this.Color * this.Alpha,
                    rotation,
                    this.Origin,
                    scale,
                    spriteEffect,
                    1.0f);
        }
    }

    public enum Facing
    {
        Left,
        Right
    }

}
