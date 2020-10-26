using ECS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Monocle
{
    class SpellLimiter : Component
    {
        public DateTime lastCast;
        public int coolDownMilliseconds;
    }
}
