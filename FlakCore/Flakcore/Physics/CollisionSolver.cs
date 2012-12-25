using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Microsoft.Xna.Framework;

namespace Flakcore.Physics
{
    public class CollisionSolver
    {
        private List<Collision> _collisions;
        private QuadTree _quadTree;

        public CollisionSolver(QuadTree quadTree)
        {
            _collisions = new List<Collision>();
            _quadTree = quadTree;
        }

        public void addCollision(Node node, string collideGroup, Func<Node, Node, bool> callback)
        {
            List<Node> collidedNodes = new List<Node>();
            _quadTree.retrieve(collidedNodes, node);

            foreach (Node collideNode in collidedNodes)
            {
                if (collideNode.isMemberOfCollisionGroup(collideGroup))
                    this.addCollision(node, collideNode, callback);
            }
        }

        public void resolveCollisions(GameTime gameTime)
        {
            foreach (Collision collision in _collisions)
            {
                collision.resolve(gameTime);
            }

            _collisions.Clear();
        }

        private void addCollision(Node node1, Node node2, Func<Node, Node, bool> callback)
        {
            if (!isAlreadyInCollisionList(node1, node2) && node1 != node2)
                _collisions.Add(new Collision(node1, node2, callback));
        }

        private bool isAlreadyInCollisionList(Node node1, Node node2)
        {
            foreach(Collision collision in _collisions)
            {
                if ((collision.node2 == node1 && collision.node1 == node2) || (collision.node1 == node1 && collision.node2 == node2))
                        return true;
            }

            return false;
        }
    }
}
