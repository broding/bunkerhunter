using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Flakcore;
using Flakcore.Display;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Flakcore.Physics;

namespace Bunker_Hunter.GameObjects.Level
{
    public class Level : Node
    {
        internal const int BLOCK_WIDTH = 32;
        internal const int BLOCK_HEIGHT = 32;
        internal const int ROOM_WIDTH = 25;
        internal const int ROOM_HEIGHT = 15;
        internal const int LEVEL_WIDTH = 2;
        internal const int LEVEL_HEIGHT = 2;

        private static Random Random = new Random();

        private List<RoomType> RoomTypes;
        private List<Room> Rooms;
        private Node RoomNode;

        public Level()
        {
            CollisionSolver.Level = this;
            Block.Graphic = GameManager.Content.Load<Texture2D>("tilemap");

            this.Rooms = new List<Room>();
            this.LoadRooms();
            this.BuildLevel();
        }

        private void LoadRooms()
        {
            DirectoryInfo dir = new DirectoryInfo(GameManager.Content.RootDirectory + "\\rooms");
            FileInfo[] files = dir.GetFiles("*.*");

            this.RoomTypes = new List<RoomType>(files.Length);

            foreach (FileInfo file in files)
            {
                string roomName = Path.GetFileNameWithoutExtension(file.Name);

                this.RoomTypes.Add(new RoomType(roomName));
            }  
        }

        private void BuildLevel()
        {
            this.RoomNode = new Node();
            this.AddChild(this.RoomNode);

            bool[,] map = new bool[Level.LEVEL_WIDTH, Level.LEVEL_HEIGHT];
            Vector2 position = Vector2.Zero;
            int currentSize = 0;

            while (currentSize != Level.LEVEL_HEIGHT * Level.LEVEL_WIDTH)
            {
                Room room = this.GetRandomRoom();
                this.PlaceRoom(room, position);

                for (int x = (int)position.X; x < (int)position.X + room.RoomType.RoomSize.X; x++)
                    for (int y = (int)position.Y; y < (int)position.Y + room.RoomType.RoomSize.Y; y++)
                        map[x, y] = true;

                position = this.GetNextBuildPosition(ref position);

                currentSize++;
            }
        }

        private Vector2 GetNextBuildPosition(ref Vector2 position)
        {
            position.Y++;

            if (position.Y == Level.LEVEL_HEIGHT)
            {
                position.X++;
                position.Y = 0;
            }

            return position;
        }

        private void PlaceRoom(Room room, Vector2 position)
        {
            this.RoomNode.AddChild(room);
            this.Rooms.Add(room);

            room.LevelPosition = position;
            room.Position = new Vector2(position.X * (ROOM_WIDTH * BLOCK_WIDTH), position.Y * (ROOM_HEIGHT * BLOCK_HEIGHT));
        }

        private Room GetRandomRoom()
        {
            return this.RoomTypes[Random.Next(0, this.RoomTypes.Count)].CreateRoom();
        }

        internal bool HasBlockCollisionGroup(string groupName)
        {
            //return this.CollisionGroups.Contains(groupName);

            return true;
        }

        internal void GetCollidedTiles(Node node, List<Node> collidedNodes)
        {
            foreach (Room room in this.Rooms)
            {
                room.GetCollidedBlocks(node, collidedNodes);
            }
        }
    }
}
