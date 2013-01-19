using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flakcore.Utils;

namespace Bunker_Hunter.Types
{
    interface ITypePool<T> where T : IPoolable 
    {
        Pool<T> Pool { get; set; }
        void InitializePool();
    }
}
