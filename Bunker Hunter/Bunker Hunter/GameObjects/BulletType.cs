using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bunker_Hunter.GameObjects
{
    public class BulletType
    {
        public int Speed { get; private set; }

        public BulletType()
        {
            this.Speed = 300;
        }
    }
}
