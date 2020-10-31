using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Monocle
{
    enum EmiterType
    {
        Point,
        Square,
        Sphere
    }
    class Emiter : Component
    {
        public EmiterType type;
        public int particleLifeTime;
        public int particleSpeed;
        public int direction;
        public int spray;
        public int particlesPerSecond;
        public Color startColor;
        public DateTime lastEmit;
        public int radius;
        public Texture2D sprite;
        public Texture2D particleTexture;
    }
}
