using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Monocle
{
    class Flammable : Component
    {
        public bool isOnFire;
        public int percentChanceToSpread;
        public int damagePerUpdate;
    }
}
