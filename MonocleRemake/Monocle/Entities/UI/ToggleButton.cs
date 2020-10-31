using ECS;
using ECS.Monocle;
using ECS.Monocle.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonocleRemake.Monocle.Entities
{
    class ToggleButton
    { 
        static public Entity Register(World w, Texture2D t, Texture2D tHeld, Vector2 pos, Vector2 size, Action<Entity> OnClick)
        {
            Entity e = w.CreateEntity()
               .AddComponent<Sprite>()
               .AddComponent<Toggle>()
               .AddComponent<Transform>()
               .AddComponent<Clickable>();

            Sprite s = e.GetComponent<Sprite>();
            s.texture = t;
            s.size = size;
            s.scale = 4;

            e.GetComponent<Transform>().position = pos;

            e.GetComponent<Clickable>().OnClick = OnClick;
            e.GetComponent<Clickable>().held = false;
            e.GetComponent<Clickable>().heldSprite = tHeld;
            e.GetComponent<Clickable>().sprite = t;
            return e;
        }
    }
}
