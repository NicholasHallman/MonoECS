using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ECS;
using Microsoft.Xna.Framework;

namespace ECS.Monocle
{
    class Sprite : Component
    {
        public Texture2D texture;
        public Vector2 size;
        public bool flipX;
        public bool flipY;
    }
}
