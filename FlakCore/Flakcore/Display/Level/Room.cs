using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Flakcore;

namespace Bunker_Hunter.GameObjects.Level
{
    class Room : Node
    {
        public RoomType RoomType;
        public Vector2 LevelPosition;

        private List<Block> Blocks;
        private Block[,] Map;

        public Room(RoomType roomType)
        {
            this.RoomType = roomType;
            this.Blocks = new List<Block>();
            this.Map = new Block[Level.ROOM_WIDTH, Level.ROOM_HEIGHT];
            this.Width = Level.BLOCK_WIDTH;
            this.Height = Level.BLOCK_HEIGHT;
        }

        public void AddBlock(int x, int y)
        {
            Block block = new Block();
            block.Position = new Vector2(x, y);
            this.Blocks.Add(block);
            this.AddChild(block);
            this.Map[x / Level.BLOCK_WIDTH, y / Level.BLOCK_HEIGHT] = block;
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix parentTransform)
        {
            foreach (Block block in this.Blocks)
            {
                Matrix globalTransform = this.getLocalTransform() * block.getLocalTransform() * GameManager.currentDrawCamera.getTransformMatrix();

                Vector2 position, scale;
                float rotation;

                Node.decomposeMatrix(ref globalTransform, out position, out rotation, out scale);

                spriteBatch.Draw(Block.Graphic, new Vector2(position.X * ScrollFactor.X, position.Y * ScrollFactor.Y), block.GetSourceRectangle(), Color.White, 0, Vector2.Zero, scale, new SpriteEffects(), 1);
            }
        }

        internal void GetCollidedBlocks(Node node, List<Node> collidedNodes)
        {
            Vector2 nodeRoomPosition = node.Position - this.Position;

            int xMin = (int)Math.Floor(nodeRoomPosition.X / Level.BLOCK_WIDTH);
            int xMax = (int)Math.Ceiling((nodeRoomPosition.X + node.Width) / Level.BLOCK_WIDTH);
            int yMin = (int)Math.Floor(nodeRoomPosition.Y / Level.BLOCK_HEIGHT);
            int yMax = (int)Math.Ceiling((nodeRoomPosition.Y + node.Height) / Level.BLOCK_HEIGHT);

            xMin = Math.Max(0, xMin - 1);
            xMax = Math.Min(Width, xMax + 1);
            yMin = Math.Max(0, yMin - 1);
            yMax = Math.Min(Height, yMax + 1);

            xMax = Math.Min(xMax, Level.ROOM_WIDTH);
            yMax = Math.Min(yMax, Level.ROOM_HEIGHT);

            for (var x = xMin; x < xMax; x++)
            {
                for (var y = yMin; y < yMax; y++)
                {
                    Block block = this.Map[x, y];
                    if (block != null)
                        collidedNodes.Add(block);
                }
            }
        }
    }
}
