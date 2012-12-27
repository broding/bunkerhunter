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
        public Node node1 { get; private set; }
        public Node node2 { get; private set; }
        public Func<Node, Node, bool> callback { get; private set; }

        private Vector2 node1VelocityDiff;
        private Vector2 node2VelocityDiff;

        private Vector2 node1PositionDiff;
        private Vector2 node2PositionDiff;

        public Collision(Node node1, Node node2, Func<Node, Node, bool> callback)
        {
            this.node1 = node1;
            this.node2 = node2;
            this.callback = callback;

            this.node1PositionDiff = Vector2.Zero;
            this.node2PositionDiff = Vector2.Zero;

            this.node1VelocityDiff = Vector2.Zero;
            this.node2VelocityDiff = Vector2.Zero;
        }

        public void resolve(GameTime gameTime)
        {
            if (!callback.Invoke(node1, node2))
                return;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Node dirtyNode1 = (Node)this.node1.Clone();
            Node dirtyNode2 = (Node)this.node2.Clone();

            dirtyNode1.Position.X += dirtyNode1.Velocity.X * deltaTime;
            dirtyNode2.Position.X += dirtyNode2.Velocity.X * deltaTime;

            dirtyNode1.RoundPosition();
            dirtyNode2.RoundPosition();

            float intersectionDepth = RectangleExtensions.GetIntersectionDepth(dirtyNode1.GetBoundingBox(), dirtyNode2.GetBoundingBox()).X;
            if (intersectionDepth != 0)
                overlapX(intersectionDepth);

            dirtyNode1 = (Node)this.node1.Clone();
            dirtyNode2 = (Node)this.node2.Clone();

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

            this.node1.RoundPosition();
            this.node2.RoundPosition();
        }

        private void CorrectNodes(float deltaTime)
        {
            if (node1PositionDiff.X != 0)
            {
                this.node1.Position.X += this.node1.Velocity.X * deltaTime;
                this.node1.Position.X += node1PositionDiff.X;
                this.node1.Velocity.X += node1VelocityDiff.X;
            }

            if (node1PositionDiff.Y != 0)
            {
                this.node1.Position.Y += this.node1.Velocity.Y * deltaTime;
                this.node1.Position.Y += node1PositionDiff.Y;
                this.node1.Velocity.Y += node1VelocityDiff.Y;
            }

            if (node2PositionDiff.X != 0)
            {
                this.node2.Position.X += this.node2.Velocity.X * deltaTime;
                this.node2.Position.X += node2PositionDiff.X;
                this.node2.Velocity.X += node2VelocityDiff.X;
            }

            if (node2PositionDiff.Y != 0)
            {
                this.node2.Position.Y += this.node2.Velocity.Y * deltaTime;
                this.node2.Position.Y += node2PositionDiff.Y;
                this.node2.Velocity.Y += node2VelocityDiff.Y;
            }
        }

        private void overlapX(float overlap)
        {
            if (overlap == 0)
                return;

            if (node1.Velocity.X != node2.Velocity.X)
            {
                if (node1.Velocity.X > node2.Velocity.X)
                {
                    node1.Touching.Right = true;
                    node2.Touching.Left = true;

                    separateX(overlap);
                }
                else if (node1.Velocity.X < node2.Velocity.X)
                {
                    node1.Touching.Left = true;
                    node2.Touching.Right = true;

                    separateX(overlap);
                }
            }
        }

        private void overlapY(float overlap)
        {
            if (overlap == 0)
                return;

            if (node1.Velocity.Y != node2.Velocity.Y)
            {
                if (node1.Velocity.Y > node2.Velocity.Y)
                {
                    node1.Touching.Bottom = true;
                    node2.Touching.Top = true;

                    separateY(overlap);
                }
                else if(node1.Velocity.Y < node2.Velocity.Y)
                {
                    node1.Touching.Top = true;
                    node2.Touching.Bottom = true;

                    separateY(overlap);
                }
            }
        }

        private void separateY(float overlap)
        {
            if (!node1.Immovable && !node2.Immovable)
            {
            }
            else if (!node1.Immovable)
            {
                this.node1PositionDiff.Y += overlap;
                this.node1VelocityDiff.Y -= node1.Velocity.Y;
            }
            else if (!node2.Immovable)
            {
                this.node2PositionDiff.Y += overlap;
                this.node2VelocityDiff.Y -= node2.Velocity.Y;
            }
        }

        private void separateX(float overlap)
        {
            if (!node1.Immovable && !node2.Immovable)
            {
            }
            else if (!node1.Immovable)
            {
                this.node1PositionDiff.X += overlap;
                this.node1VelocityDiff.X -= node1.Velocity.X;
            }
            else if (!node2.Immovable)
            {
                this.node2PositionDiff.X += overlap;
                this.node2VelocityDiff.X -= node2.Velocity.X;
            }
        }
    }
}
