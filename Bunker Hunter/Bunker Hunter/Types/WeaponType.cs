using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bunker_Hunter.Types
{
    public class WeaponType
    {
        public int FireRate;
        public BulletType BulletType;

        public WeaponType()
        {
            this.BulletType = new BulletType();
            this.FireRate = 100;
        }
    }
}
