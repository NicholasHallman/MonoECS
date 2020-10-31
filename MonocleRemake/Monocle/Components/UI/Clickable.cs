using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Monocle.UI
{
    class Clickable : Component
    {
        public Action<Entity> OnClick;
        public bool held;
        public Texture2D heldSprite;
        public Texture2D sprite;
    }
}
