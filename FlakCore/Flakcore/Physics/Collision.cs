using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Microsoft.Xna.Framework;
using Flakcore.Utils;
using Display.Tilemap;

namespace Flakcore.Physics
{
    class Collision
    {
        public Node Node1 { get; private set; }
        public Node Node2 { get; private set; }
        public Action<Node, Node> Callback { get; private set; }
        public Func<Node, Node, bool> Checker { get; private set; }

        private Vector2 node1VelocityDiff;
        private Vector2 node2VelocityDiff;

        private Vector2 node1PositionDiff;
        private Vector2 node2PositionDiff;

        public Collision(Node node1, Node node2, Action<Node, Node> callback, Func<Node, Node, bool> checker)
        {
            this.Node1 = node1;
            this.Node2 = node2;
            this.Callback = callback;
            this.Checker = checker;

            this.node1PositionDiff = Vector2.Zero;
            this.node2PositionDiff = Vector2.Zero;

            this.node1VelocityDiff = Vector2.Zero;
            this.node2VelocityDiff = Vector2.Zero;
        }

        public void resolve(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.IsCollision(deltaTime))
            {
                if(this.Checker != null)
                    if(!this.Checker(this.Node1, this.Node2))
                        return;

                Node dirtyNode1 = (Node)this.Node1.Clone();
                Node dirtyNode2 = (Node)this.Node2.Clone();

                dirtyNode1.Position.X += dirtyNode1.Velocity.X * deltaTime;
                dirtyNode2.Position.X += dirtyNode2.Velocity.X * deltaTime;

                dirtyNode1.RoundPosition();
                dirtyNode2.RoundPosition();

                float intersectionDepth = RectangleExtensions.GetIntersectionDepth(dirtyNode1.GetBoundingBox(), dirtyNode2.GetBoundingBox()).X;
                if (intersectionDepth != 0)
                    overlapX(intersectionDepth);

                dirtyNode1 = (Node)this.Node1.Clone();
                dirtyNode2 = (Node)this.Node2.Clone();

                dirtyNode1.Position.Y += dirtyNode1.Velocity.Y * deltaTime;
                dirtyNode2.Position.Y += dirtyNode2.Velocity.Y * deltaTime;

                dirtyNode1.RoundPosition();
                dirtyNode2.RoundPosition();

                BoundingRectangle box1 = dirtyNode1.GetBoundingBox();
                BoundingRectangle box2 = dirtyNode2.GetBoundingBox();
                intersectionDepth = RectangleExtensions.GetIntersectionDepth(dirtyNode1.GetBoundingBox(), dirtyNode2.GetBoundingBox()).Y;
                if (intersectionDepth != 0)
                    overlapY(intersectionDepth);

                this.CorrectNodes(deltaTime);

                this.Node1.RoundPosition();
                this.Node2.RoundPosition();

                if(this.Callback != null)
                    this.Callback(Node1, Node2);
            }
        }

        private bool IsCollision(float deltaTime)
        {
            Node dirtyNode1 = (Node)this.Node1.Clone();
            Node dirtyNode2 = (Node)this.Node2.Clone();

            dirtyNode1.Position += dirtyNode1.Velocity * deltaTime;
            dirtyNode2.Position += dirtyNode2.Velocity * deltaTime;

            dirtyNode1.RoundPosition();
            dirtyNode2.RoundPosition();

            return dirtyNode1.GetBoundingBox().Intersects(dirtyNode2.GetBoundingBox());
        }

        private void CorrectNodes(float deltaTime)
        {
            if (node1PositionDiff.X != 0)
            {
                this.Node1.Position.X += this.Node1.Velocity.X * deltaTime;
                this.Node1.Position.X += node1PositionDiff.X;
                this.Node1.Velocity.X += node1VelocityDiff.X;
            }

            if (node1PositionDiff.Y != 0)
            {
                this.Node1.Position.Y += this.Node1.Velocity.Y * deltaTime;
                this.Node1.Position.Y += node1PositionDiff.Y;
                this.Node1.Velocity.Y += node1VelocityDiff.Y;
            }

            if (node2PositionDiff.X != 0)
            {
                this.Node2.Position.X += this.Node2.Velocity.X * deltaTime;
                this.Node2.Position.X += node2PositionDiff.X;
                this.Node2.Velocity.X += node2VelocityDiff.X;
            }

            if (node2PositionDiff.Y != 0)
            {
                this.Node2.Position.Y += this.Node2.Velocity.Y * deltaTime;
                this.Node2.Position.Y += node2PositionDiff.Y;
                this.Node2.Velocity.Y += node2VelocityDiff.Y;
            }
        }

        private void overlapX(float overlap)
        {
            if (overlap == 0)
                return;

            if (Node1.Velocity.X != Node2.Velocity.X)
            {
                if (Node1.Velocity.X > Node2.Velocity.X)
                {
                    Node1.Touching.Right = true;
                    Node2.Touching.Left = true;

                    separateX(overlap);
                }
                else if (Node1.Velocity.X < Node2.Velocity.X)
                {
                    Node1.Touching.Left = true;
                    Node2.Touching.Right = true;

                    separateX(overlap);
                }
            }
        }

        private void overlapY(float overlap)
        {
            if (overlap == 0)
                return;

            if (Node1.Velocity.Y != Node2.Velocity.Y)
            {
                if (Node1.Velocity.Y > Node2.Velocity.Y)
                {
                    Node1.Touching.Bottom = true;
                    Node2.Touching.Top = true;

                    separateY(overlap);
                }
                else if(Node1.Velocity.Y < Node2.Velocity.Y)
                {
                    Node1.Touching.Top = true;
                    Node2.Touching.Bottom = true;

                    separateY(overlap);
                }
            }
        }

        private void separateY(float overlap)
        {
            if (!Node1.Immovable && !Node2.Immovable)
            {
            }
            else if (!Node1.Immovable)
            {
                this.node1PositionDiff.Y += overlap;
                this.node1VelocityDiff.Y -= Node1.Velocity.Y;
            }
            else if (!Node2.Immovable)
            {
                this.node2PositionDiff.Y += overlap;
                this.node2VelocityDiff.Y -= Node2.Velocity.Y;
            }
        }

        private void separateX(float overlap)
        {
            if (!Node1.Immovable && !Node2.Immovable)
            {
            }
            else if (!Node1.Immovable)
            {
                this.node1PositionDiff.X += overlap;
                this.node1VelocityDiff.X -= Node1.Velocity.X;
            }
            else if (!Node2.Immovable)
            {
                this.node2PositionDiff.X += overlap;
                this.node2VelocityDiff.X -= Node2.Velocity.X;
            }
        }
    }
}
