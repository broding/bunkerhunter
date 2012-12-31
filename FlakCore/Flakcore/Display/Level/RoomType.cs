using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Display.Tilemap;
using Microsoft.Xna.Framework;

namespace Bunker_Hunter.GameObjects.Level
{
    class RoomType
    {
        private Tilemap Tilemap;

        public RoomType(string roomName)
        {
            this.Tilemap = new Tilemap();
            this.Tilemap.LoadMap(@"Content/rooms/" + roomName + ".tmx", 32, 32);
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
