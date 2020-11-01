using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Monocle
{
    class SpriteSheet : Component
    {
        public Vector2 SpriteSize;
        public Dictionary<string, Vector2> SpriteNames;
    }
}
