using ECS;
using ECS.Monocle;
using ECS.Monocle.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading.Tasks;

namespace MonocleRemake.Monocle.Services.UI
{
    class ClickDetect : Service
    {
        Type[] componentTypes = new Type[] { typeof(Clickable), typeof(Transform), typeof(Sprite)};

        public ClickDetect()
        {
            query = Query.All(componentTypes);
        }

        private static bool InBounds(Vector2 cursor, Rectangle b)
        {
            return b.Contains(cursor.X, cursor.Y);
        }

        public override void Execute(Entity[] entities, World w)
        {
            MouseState state = Mouse.GetState();
            bool leftDown = state.LeftButton == ButtonState.Pressed;
            bool leftUp = state.LeftButton == ButtonState.Released;
            Vector2 mousePos = new Vector2(state.X, state.Y);

            Parallel.ForEach(entities, entity =>
            {
                Transform t = entity.GetComponent<Transform>();
                Sprite s = entity.GetComponent<Sprite>();
                Clickable click = entity.GetComponent<Clickable>();
                Vector2 size = new Vector2(s.size.X * s.scale, s.size.Y * s.scale);
                Rectangle bounds = new Rectangle((int)t.position.X - (int)(size.X / 2), (int)t.position.Y - (int)(size.Y / 2), (int)size.X, (int)size.Y);
                if(!click.held && leftDown && InBounds(mousePos, bounds))
                {
                    click.held = true;
                    if (click.heldSprite != null)
                    {
                        s.texture = click.heldSprite;
                    }
                } 
                else if (leftUp && InBounds(mousePos, bounds) && click.held)
                {
                    click.held = false;
                    if (click.sprite != null)
                    {
                        s.texture = click.sprite;
                        click.OnClick(entity);
                    }
                }
            });
        }
    }
}
