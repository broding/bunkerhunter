using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bunker_Hunter.Types
{
    class EnemyBehaviorType
    {
        public int BehaviorUpdate;
        public bool Aggresive;
        public bool Runs;
        public int RunSpeed;
        public bool Jumps;
        public int JumpInterval;

        public EnemyBehaviorType()
        {
            this.BehaviorUpdate = 1000;
            this.Aggresive = false;
            this.Runs = true;
            this.RunSpeed = 120;
            this.Jumps = true;
            this.JumpInterval = 3000;
        }
    }
}
