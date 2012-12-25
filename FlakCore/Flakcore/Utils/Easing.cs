using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RC_Management_Game.Activities
{
    class Easing
    {
        public static float Linear(float time, float startValue, float change, float duration)
        {
            return change * time / duration + startValue;
        }
    }
}