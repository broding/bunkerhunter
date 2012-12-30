using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Microsoft.Xna.Framework;
using Display.Tilemap;

namespace Flakcore.Physics
{
    public class CollisionSolver
    {
        public static List<Tilemap> Tilemaps { get; private set; }

        private List<Collision> Collisions;
        private QuadTree QuadTree;

        public CollisionSolver(QuadTree quadTree)
        {
            Tilemaps = new List<Tilemap>();

            this.Collisions = new List<Collision>();
            this.QuadTree = quadTree;
        }

        public void addCollision(Node node, string collideGroup, Action<Node, Node> callback)
        {
            List<Node> collidedNodes = new List<Node>();

            if (this.IsTilemapCollision(collideGroup))
            {
                this.GetCollidedTiles(node, collidedNodes);
            }
            else
            {
                QuadTree.retrieve(collidedNodes, node);
            }


            foreach (Node collideNode in collidedNodes)
            {
                if (collideNode.isMemberOfCollisionGroup(collideGroup))
                    this.addCollision(node, collideNode, callback);
            }

            
        }

        private bool IsTilemapCollision(string collideGroup)
        {
            foreach (Tilemap tilemap in Tilemaps)
            {
                if(tilemap.HasTileCollisionGroup(collideGroup))
                    return true;
            }

            return false;
        }

        private void GetCollidedTiles(Node node, List<Node> collidedNodes)
        {
            foreach (Tilemap tilemap in Tilemaps)
            {
                tilemap.GetCollidedTiles(node, collidedNodes);
            }
        }

        public void resolveCollisions(GameTime gameTime)
        {
            foreach (Collision collision in Collisions)
            {
                collision.resolve(gameTime);
            }

            Collisions.Clear();
        }

        private void addCollision(Node node1, Node node2, Action<Node, Node> callback)
        {
            if (!isAlreadyInCollisionList(node1, node2) && node1 != node2)
                Collisions.Add(new Collision(node1, node2, callback));
        }

        private bool isAlreadyInCollisionList(Node node1, Node node2)
        {
            foreach(Collision collision in Collisions)
            {
                if ((collision.node2 == node1 && collision.node1 == node2) || (collision.node1 == node1 && collision.node2 == node2))
                        return true;
            }

            return false;
        }
    }
}
