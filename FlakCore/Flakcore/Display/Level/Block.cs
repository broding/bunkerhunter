using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Display;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Flakcore.Physics;

namespace Flakcore.Display.Level
{
    class Block : Sprite
    {
        public static Texture2D Graphic { get; set; }
        public static Texture2D BorderGraphic { get; set; }

        public BlockType Type { get; private set; }
        public Room Room { get; private set; }

        private Sides Borders;

        public Block(BlockType type, Room room)
        {
            this.LoadTexture(Block.Graphic, Level.BLOCK_WIDTH, Level.BLOCK_HEIGHT);
            this.Type = type;
            this.Collidable = false;
            this.Immovable = true;
            this.Borders = new Sides();
            this.SetupBlock();
            this.Room = room;
        }

        private void SetupBlock()
        {
            switch (this.Type)
            {
                case BlockType.WALL:
                    this.AddCollisionGroup("tilemap");
                    this.SourceRectangle = new Rectangle(32, 0, 32, 32);
                    this.Depth = 0.2f;
                    break;
                case BlockType.LADDERAREA:
                case BlockType.LADDER:
                    this.AddCollisionGroup("ladderArea");
                    this.SourceRectangle = new Rectangle(64, 0, 32, 32);
                    this.CollidableSides.SetAllFalse();
                    this.CollidableSides.Top = true;
                    break;
            }
        }

        internal void SetBorders(Sides borders)
        {
            this.Borders = borders;

            if (!this.Borders.Top && this.Type == BlockType.WALL)
            {
                Sprite topBorder = new Sprite();
                topBorder.LoadTexture(Block.BorderGraphic);
                topBorder.Origin = new Vector2(11, 12);
                topBorder.Rotation = 0;
                topBorder.Position = new Vector2(Level.BLOCK_WIDTH / 2, Level.BLOCK_HEIGHT / 2);
                this.AddChild(topBorder);
            }

            if (!this.Borders.Bottom && this.Type == BlockType.WALL)
            {
                Sprite bottomBorder = new Sprite();
                bottomBorder.LoadTexture(Block.BorderGraphic);
                bottomBorder.Origin = new Vector2(11, 12);
                bottomBorder.Rotation = (float)Math.PI;
                bottomBorder.Position = new Vector2(Level.BLOCK_WIDTH / 2,Level.BLOCK_HEIGHT / 2);
                this.AddChild(bottomBorder);
            }

            if (!this.Borders.Left && this.Type == BlockType.WALL)
            {
                Sprite leftBorder = new Sprite();
                leftBorder.LoadTexture(Block.BorderGraphic);
                leftBorder.Origin = new Vector2(11, 12);
                leftBorder.Rotation = (float)Math.PI / 2 + (float)Math.PI;
                leftBorder.Position = new Vector2(Level.BLOCK_WIDTH / 2, Level.BLOCK_HEIGHT / 2);
                this.AddChild(leftBorder);
            }

            if (!this.Borders.Right && this.Type == BlockType.WALL)
            {
                Sprite rightBorder = new Sprite();
                rightBorder.LoadTexture(Block.BorderGraphic);
                rightBorder.Origin = new Vector2(11, 12);
                rightBorder.Rotation = (float)Math.PI / 2;
                rightBorder.Position = new Vector2(Level.BLOCK_WIDTH / 2, Level.BLOCK_HEIGHT / 2);
                this.AddChild(rightBorder);
            }
        }

        public Vector2 RoomPosition
        {
            get
            {
                return new Vector2((float)Math.Floor((float)this.Position.X / Level.BLOCK_WIDTH), (float)Math.Floor((float)this.Position.Y / Level.BLOCK_HEIGHT));
            }
        }

        public Vector2 LevelPosition
        {
            get
            {
                Vector2 roomPosition = this.RoomPosition;
                return new Vector2(this.Room.LevelPosition.X * Level.ROOM_WIDTH + roomPosition.X, this.Room.LevelPosition.Y * Level.ROOM_HEIGHT + roomPosition.Y);
            }
        }

        public static BlockType GetBlockTypeFromString(string type)
        {
            switch (type)
            {
                case "0":
                    return BlockType.WALL;
                case "1":
                    return BlockType.LADDERAREA;
                case "L":
                    return BlockType.LADDER;
                case "R":
                    return BlockType.REWARD;
                case "S":
                    return BlockType.START;
                case "E":
                    return BlockType.END;
            }

            return BlockType.EMPTY;
        }
    }

    public enum BlockType
    {
        EMPTY,
        WALL,
        LADDERAREA,
        LADDER,
        START,
        END,
        REWARD

    }
}
