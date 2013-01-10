using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Flakcore.Utils;

namespace Flakcore.Display
{
    public class Sprite : Node
    {
        public Texture2D Texture { get; protected set; }
        public Facing Facing;
        public Color Color;
        public float Alpha;
        public Rectangle SourceRectangle;
        public OffScreenAction OffScreenAction;
        public SpriteEffects SpriteEffects;

        private List<Animation> Animations;
        private bool Animating;
        private double AnimationTimer;
        private int CurrentFrame;
        private Animation CurrentAnimation;

        protected static Vector2 DrawPosition, DrawScale;
        protected static Rectangle DrawRectangle;

        public Sprite() : base()
        {
            Animations = new List<Animation>();
            Facing = Facing.Right;
            Color = Color.White;
            Alpha = 1;
            OffScreenAction = OffScreenAction.NO_DRAW;
            SpriteEffects = new SpriteEffects();

            if (DrawPosition != null)
                Sprite.DrawPosition = Vector2.Zero;

            if (DrawScale != null)
                Sprite.DrawScale = Vector2.Zero;

            if (DrawRectangle != null)
                Sprite.DrawRectangle = Rectangle.Empty;
        }

        public void LoadTexture(string assetName)
        {
            this.LoadTexture(GameManager.Content.Load<Texture2D>(assetName));
        }

        public void LoadTexture(Texture2D texture)
        {
            this.LoadTexture(texture, texture.Width, texture.Height);
        }

        public virtual void LoadTexture(Texture2D texture, int width, int height)
        {
            this.Texture = texture;
            this.Width = width;
            this.Height = height;
            this.Animating = false;
            this.SourceRectangle = new Rectangle(0, 0, width, height);
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

        public override void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
        {
            if (!Visable || !GameManager.currentDrawCamera.BoundingBox.Intersects(this.GetBoundingBox(this.Position + parentPosition)))
                return;

            this.SpriteEffects = SpriteEffects.None;

            if(Facing == Facing.Left)
                this.SpriteEffects = SpriteEffects.FlipHorizontally;

            this.DrawCall(spriteBatch, parentPosition + this.Position);

            base.Draw(spriteBatch, this.Position + parentPosition);   
        }

        protected override void DrawCall(SpriteBatch spriteBatch, Vector2 position)
        {
            if (this.Texture == null)
                return;

            Sprite.DrawPosition.X = position.X * this.ScrollFactor.X;
            Sprite.DrawPosition.Y = position.Y * this.ScrollFactor.Y;

            if (Animating)
                spriteBatch.Draw(Texture,
                    Sprite.DrawPosition,
                    new Rectangle(CurrentAnimation.frames[CurrentFrame] * Width, 0, Width, Height),
                    this.Color * this.Alpha,
                    this.Rotation,
                    this.Origin,
                    this.Scale,
                    this.SpriteEffects,
                    Node.GetDrawDepth(this.GetParentDepth()));
            else
                spriteBatch.Draw(Texture,
                    Sprite.DrawPosition,
                    this.SourceRectangle,
                    this.Color * this.Alpha,
                    this.Rotation,
                    this.Origin,
                    this.Scale,
                    this.SpriteEffects,
                    Node.GetDrawDepth(this.GetParentDepth()));
        }

        private bool OffScreen()
        {
            switch (this.OffScreenAction)
            {
                case OffScreenAction.NO_DRAW:
                    return true;
                case OffScreenAction.KILL:
                    this.Kill();
                    return true;
                case OffScreenAction.NONE:
                    return false;
                default:
                    return false;
            }

        }
    }

    public enum Facing
    {
        Left,
        Right
    }

    public enum OffScreenAction
    {
        NONE,
        NO_DRAW,
        KILL
    }

}
