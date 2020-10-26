using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Monocle
{
    class Emiter : Component
    {
        public int particleLifeTime;
        public int particleSpeed;
        public int direction;
        public int spray;
        public int particlesPerSecond;
        public Color color;
        public DateTime lastEmit;
    }
}
