using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Monocle
{
    class Controller : Component
    {
        public DateTime lockedAt;
        public int lockedFor;
    }
}
