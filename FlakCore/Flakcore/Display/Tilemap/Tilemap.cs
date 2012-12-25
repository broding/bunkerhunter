using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Flakcore.Display;
using Flakcore;
using Flakcore.Utils;

namespace Display.Tilemap
{
    public class Tilemap : Node
    {
        private int _width;
        private int _height;
        public static int tileWidth { get; private set; }
        public static int tileHeight { get; private set; }

        private List<Layer> _layers;
        private List<Tileset> _tilesets;

        public Tilemap()
        {
            _layers = new List<Layer>();
            _tilesets = new List<Tileset>();
        }

        public void loadMap(string path, int tileWidth, int tileHeight)
        {
            Tilemap.tileWidth = tileWidth;
            Tilemap.tileHeight = tileHeight;

            XDocument doc = XDocument.Load(path);

            _width = Convert.ToInt32(doc.Element("map").Attribute("width").Value);
            _height = Convert.ToInt32(doc.Element("map").Attribute("width").Value);

            // load all tilesets
            foreach (XElement element in doc.Descendants("tileset"))
            {
                string assetName = element.Element("image").Attribute("source").Value;
                assetName = Path.GetFileNameWithoutExtension(assetName);

                // load all collisionGroups from this tileset ('collisionGroups' from different tiles)
                string[] tileCollisionGroups = new string[100];


                foreach (XElement tile in element.Descendants("tile"))
                {
                    if (tile.Descendants("property").First().Attribute("name").Value == "collisionGroups")
                        tileCollisionGroups[(int)tile.Attribute("id")] = tile.Descendants("property").First().Attribute("value").Value;
                }

                Tileset tileset = new Tileset(Convert.ToInt32(element.Attribute("firstgid").Value), element.Attribute("name").Value, Convert.ToInt32(element.Element("image").Attribute("width").Value), Convert.ToInt32(element.Element("image").Attribute("height").Value), GameManager.content.Load<Texture2D>(assetName), tileCollisionGroups);

                _tilesets.Add(tileset);
            }

            // load all layers
            foreach (XElement element in doc.Descendants("layer"))
            {
                Layer layer = new Layer(element.Attribute("name").Value, Convert.ToInt32(element.Attribute("width").Value), Convert.ToInt32(element.Attribute("height").Value), this);

                int x = 0;
                int y = 0;

                foreach (XElement tile in element.Descendants("tile"))
                {
                    if (Convert.ToInt32(tile.Attribute("gid").Value) == 0)
                    {
                        x++;
                        if (x >= layer.Width)
                        {
                            y++;
                            x = 0;
                        }
                        continue;
                    }

                    layer.addTile(Convert.ToInt32(tile.Attribute("gid").Value), x, y, getCorrectTileset(Convert.ToInt32(tile.Attribute("gid").Value)));
                    x++;

                    // check if y needs to be incremented
                    if (x >= layer.Width)
                    {
                        y++;
                        x = 0;
                    }
                }


                _layers.Add(layer);
            }
        }

        private Tileset getCorrectTileset(int gid)
        {
            Tileset best = null;

            foreach (Tileset tileset in _tilesets)
            {
                if (best == null)
                    best = tileset;
                else
                {
                    if (gid >= tileset.firstGid && tileset.firstGid > best.firstGid)
                        best = tileset;
                }
            }

            return best;
        }

        public override void Draw(SpriteBatch spriteBatch, Matrix parentTransform)
        {
            // loop through all layers to draw them
            foreach (Layer layer in _layers)
            {
                layer.Draw(spriteBatch, Matrix.Identity);
            }
        }

        public override List<Node> getAllChildren(List<Node> nodes)
        {
            foreach (Layer layer in _layers)
            {
                layer.getAllChildren(nodes);
            }

            return nodes;
        }
    }
}
