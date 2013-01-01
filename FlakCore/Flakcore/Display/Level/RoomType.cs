using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Display.Tilemap;
using Microsoft.Xna.Framework;
using Flakcore.Physics;

namespace Bunker_Hunter.GameObjects.Level
{
    class RoomType
    {
        public Sides Sides;
        private Tilemap Tilemap;

        public RoomType(string roomName)
        {
            this.Tilemap = new Tilemap();
            this.Tilemap.LoadMap(@"Content/rooms/" + roomName + ".tmx", 32, 32);
            this.Sides = new Sides();

            this.CheckSides();
        }

        private void CheckSides()
        {
            foreach (KeyValuePair<string, string> property in this.Tilemap.Properties)
            {
                switch (property.Key)
                {
                    case "left":
                        this.Sides.Left = true;
                        break;
                    case "right":
                        this.Sides.Right = true;
                        break;
                    case "top":
                        this.Sides.Top = true;
                        break;
                    case "bottom":
                        this.Sides.Bottom = true;
                        break;
                }
            }
        }

        public Room CreateRoom()
        {
            Room room = new Room(this);
            TileLayer structureLayer = this.Tilemap.GetLayer("structure");

            foreach (Tile tile in structureLayer.Tiles)
            {
                room.AddBlock((int)tile.Position.X, (int)tile.Position.Y);
            }

            return room;
        }

        public Vector2 RoomSize
        {
            get
            {
                return new Vector2(this.Tilemap.Width / Level.ROOM_WIDTH, this.Tilemap.Height / Level.ROOM_HEIGHT);
            }
        }
    }
}
