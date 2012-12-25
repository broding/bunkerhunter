using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Flakcore.Utils;
using Flakcore.Physics;

namespace Flakcore.Display
{
    public class Node : ICloneable
    {
        public List<Node> Children { get; protected set; }

        public Node Parent;

        public Vector2 position = Vector2.Zero;
        public Vector2 previousPosition { get; protected set; }
        public Vector2 Velocity = Vector2.Zero;
        public Vector2 previousVelocity { get; protected set; }
        public Vector2 Acceleration = Vector2.Zero;

        public int Width;
        public int Height;

        public Vector2 Origin = Vector2.Zero;
        public Vector2 Scale = Vector2.One;

        public float Rotation = 0;
        public float RotationVelocity = 0;

        public Vector2 ScrollFactor = Vector2.One;

        public Sides Touching;
        public Sides WasTouching;

        public Vector2 MaxVelocity = Vector2.Zero;
        public float Elasticity = 0f;
        public bool Visable;
        public bool Immovable;
        public bool Dead { get; protected set; }

        private List<string> _collisionGroup;

        public Node()
        {
            Children = new List<Node>();
            _collisionGroup = new List<string>();
            this.Touching = new Sides();
            this.WasTouching = new Sides();
            Visable = true;
        }

        public void addChild(Node child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        public void removeChild(Node child)
        {
            if (!Children.Remove(child))
                throw new Exception("Tried to remove child but gave an error");
        }

        public virtual void Update(GameTime gameTime)
        {
            if (this.Dead)
                return;

            foreach (Node child in Children.ToList<Node>())
                child.Update(gameTime);

            WasTouching = Touching;
            Touching = new Sides();
        }

        public virtual void PostUpdate(GameTime gameTime)
        {
            foreach (Node child in Children.ToList<Node>())
                child.PostUpdate(gameTime);

            if (!Immovable)
            {
                this.Rotation += RotationVelocity;
                this.position += this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                //his.RoundPosition();
                //this.RoundVelocity();
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            this.Draw(spriteBatch, Matrix.Identity);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Matrix parentTransform)
        {
            if (!Visable || Dead)
                return;

            Matrix globalTransform = this.getLocalTransform() * parentTransform;

            foreach (Node child in Children)
            {
                child.Draw(spriteBatch, globalTransform);
            }
        }

        protected virtual void DrawCall(SpriteBatch spriteBatch, Vector2 position, Vector2 scale, float rotation, SpriteEffects spriteEffect)
        {
        }

        public void removeAllChildren()
        {
            Children = null;
            Children = new List<Node>();
        }

        public virtual BoundingRectangle getBoundingBox()
        {
            Vector2 worldPosition = this.getWorldPosition();

            return new BoundingRectangle(worldPosition.X, worldPosition.Y, Width, Height);
        }

        public Vector2 getWorldPosition()
        {
            if (Parent == null)
                return position;
            else
                return Parent.getWorldPosition() + position;
        }

        public virtual void Kill()
        {
            this.Dead = true;
            this.Visable = false;
        }

        public virtual void Revive()
        {
            this.Dead = false;
            this.Visable = true;
        }

        public Matrix getLocalTransform()
        {
            // Transform = -Origin * Scale * Rotation * Translation
            return Matrix.CreateTranslation(-Origin.X, -Origin.Y, 0f) *
            //Matrix.CreateScale(Scale.X, Scale.Y, 1f) *
            //Matrix.CreateRotationZ(Rotation) *   
            Matrix.CreateTranslation(position.X, position.Y, 0f);
        }

        public static void decomposeMatrix(ref Matrix matrix, out Vector2 position, out float rotation, out Vector2 scale)
        {
            Vector3 position3, scale3;
            Quaternion rotationQ;
            matrix.Decompose(out scale3, out rotationQ, out position3);
            Vector2 direction = Vector2.Transform(Vector2.UnitX, rotationQ);
            rotation = (float)Math.Atan2(direction.Y, direction.X);
            position = new Vector2(position3.X, position3.Y);
            scale = new Vector2(scale3.X, scale3.Y);
        }

        public virtual List<Node> getAllChildren(List<Node> nodes)
        {
            nodes.Add(this);

            if (Children.Count == 0)
                return nodes;
            else
            {
                foreach (Node child in Children)
                {
                    child.getAllChildren(nodes);
                }

                return nodes;
            }
        }

        public void RoundPosition()
        {
            this.position.X = (float)Math.Round(this.position.X);
            this.position.Y = (float)Math.Round(this.position.Y);
        }

        public void RoundVelocity()
        {
            this.Velocity.X = (float)Math.Round(this.Velocity.X);
            this.Velocity.Y = (float)Math.Round(this.Velocity.Y);
        }

        public void addCollisionGroup(string groupName)
        {
            this._collisionGroup.Add(groupName);
        }

        public void removeCollisionGroup(string groupName)
        {
            this._collisionGroup.Remove(groupName);
        }

        public bool isMemberOfCollisionGroup(string groupName)
        {
            return this._collisionGroup.Contains(groupName);
        }

        public bool hasCollisionGroup()
        {
            return this._collisionGroup.Count > 0;
        }


        public object Clone()
        {
            Node clone = new Node();
            clone.position = new Vector2(position.X, position.Y);
            clone.Velocity = new Vector2(Velocity.X, Velocity.Y);
            clone.Width = Width;
            clone.Height = Height;

            return clone;
        }
    }
}
