using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flakcore.Display.Level.LevelObjects
{
    class Ladder : LevelObject
    {
        public Ladder()
        {
            this.LoadTexture("ladder");
            this.AddCollisionGroup("ladder");
            this.Immovable = true;
        }
    }
}
