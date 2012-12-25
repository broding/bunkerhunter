using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Flakcore.Display;
using Microsoft.Xna.Framework;
using Flakcore;

namespace Display.Tilemap
{
    public class Layer : Node
    {
        public string name;

        private Tilemap _tilemap;

        private List<Tile> _tiles;
        private Tile[,] _map;

        public Tile[,] map
        {
            get
            {
                return _map;
            }
        }

        public Layer(string name, int width, int height, Tilemap tilemap)
        {
            this.name = name;
            this.Width = width;
            this.Height = height;
            this._tilemap = tilemap;

            _map = new Tile[width, height];
            _tiles = new List<Tile>();
        }

        public void addTile(int gid, int x, int y, Tileset tileset)
        {
            gid--; // remove 1 because the Tiled editor start counting at 1, instead of 0

            string[] collisionGroups = new string[10];
            if(tileset.collisionGroups[gid] != null)
                collisionGroups = tileset.collisionGroups[gid].Split(' ');

            int sourceY = ((gid * Tilemap.tileWidth) / (tileset.width)) * Tilemap.tileHeight;
            Rectangle sourceRect = new Rectangle(gid * Tilemap.tileWidth - ((sourceY / Tilemap.tileHeight) * tileset.width), sourceY, Tilemap.tileWidth, Tilemap.tileHeight);

            map[x, y] = new Tile(x, y, gid, sourceRect, tileset, collisionGroups);
            _tiles.Add(map[x, y]);
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix parentTransform)
        {
            foreach (Tile tile in _tiles)
            {
                Matrix globalTransform = tile.getLocalTransform() * GameManager.currentDrawCamera.getTransformMatrix();

                Vector2 position, scale;
                float rotation;

                Node.decomposeMatrix(ref globalTransform, out position, out rotation, out scale);

                //spriteBatch.Draw(tile.tileset.graphic, position, tile.sourceRect, Color.White);
                spriteBatch.Draw(tile.tileset.graphic, new Vector2(position.X * ScrollFactor.X, position.Y * ScrollFactor.Y), tile.sourceRect, Color.White, 0, Vector2.Zero, scale, new SpriteEffects(), 1);
            }
        }

        public override List<Node> getAllChildren(List<Node> nodes)
        {
            foreach (Tile tile in _tiles)
                nodes.Add(tile);

            return nodes;
        }
    }
}
