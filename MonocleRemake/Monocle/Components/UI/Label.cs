using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Monocle.UI
{
    class Label : Component
    {
        public string text;
        public SpriteFont spriteFont;
        public Color color;
    }
}
