using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flakcore.Display.Level.LevelObjects
{
    class Start : LevelObject
    {
        public Start()
        {
            this.LoadTexture("start");
            this.Immovable = true;
        }
    }
}
