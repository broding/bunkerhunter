using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Display.Tilemap
{
    public class Tileset
    {
        private int _firstGid;
        public int firstGid { get { return this._firstGid; } }

        private string _name;
        public string name { get { return this._name; } }

        private int _width;
        public int width { get { return this._width; } }

        private int _height;
        public int height { get { return this._height; } }

        private Texture2D _graphic;
        public Texture2D graphic { get { return this._graphic; } }

        public string[] collisionGroups { get; private set; }

        public Tileset(int firstGid, string name, int width, int height, Texture2D graphic, string[] collisionGroups)
        {
            this._firstGid = firstGid;
            this._name = name;
            this._width = width;
            this._height = height;
            this._graphic = graphic;
            this.collisionGroups = collisionGroups;
        }
    }
}
