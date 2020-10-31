using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Phone.Notification.Management;

namespace MonocleRemake.Monocle.Services
{
    class PlayerController : Service
    {
        private static Type[] QueryComponents = { 
            typeof(PlayerTag),
        };

        public PlayerController()
        {
            query = new Query(QueryComponents);
        }

        override public void Execute(Entity[] entities, World w)
        {
            KeyboardState state = Keyboard.GetState();

            foreach (Entity entity in entities)
            {
                Transform transform = entity.GetComponent<Transform>();
                Sprite sprite = entity.GetComponent<Sprite>();
                Controller controller = entity.GetComponent<Controller>();

                var lockedAt = controller.lockedAt;
                var lockedFor = controller.lockedFor;
                bool isUnlocked = DateTime.Now.CompareTo(lockedAt.AddMilliseconds(lockedFor * 16)) >= 0;

                if (lockedAt != DateTime.MinValue && isUnlocked) lockedAt = DateTime.MinValue;

                if (lockedAt == DateTime.MinValue || isUnlocked)
                {

                    Vector2 newDir = new Vector2()
                    {
                        X = state.IsKeyDown(Keys.D) && state.IsKeyDown(Keys.A) ? 0 : state.IsKeyDown(Keys.D) ? 1 : state.IsKeyDown(Keys.A) ? -1 : 0,
                        Y = state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.S) ? 0 : state.IsKeyDown(Keys.W) ? -1 : state.IsKeyDown(Keys.S) ? 1 : 0
                    };

                    if (Math.Abs(newDir.X) + Math.Abs(newDir.Y) == 2)
                    {
                        newDir.X /= 1.5f;
                        newDir.Y /= 1.5f;
                    }

                    if (newDir.X != 0 || newDir.Y != 0)
                    {
                        transform.direction = newDir;
                        transform.speed = 4;
                    }
                    else
                    {
                        transform.speed = 0;
                    }

                    if (state.IsKeyDown(Keys.D))
                    {
                        sprite.flipX = false;
                    }
                    if (state.IsKeyDown(Keys.A))
                    {
                        sprite.flipX = true;
                    }

                    if (state.IsKeyDown(Keys.Space))
                    {
                        SpellLimiter sl = entity.GetComponent<SpellLimiter>();
                        if (DateTime.Now.CompareTo(sl.lastCast.AddMilliseconds(sl.coolDownMilliseconds)) > 0)
                        {
                            controller.lockedAt = DateTime.Now;
                            controller.lockedFor = 10;
                            Entity fireball = Fireball.Register(w, transform.position, transform.direction);
                            fireball.GetComponent<Transform>().speed += transform.speed;
                            sl.lastCast = DateTime.Now;
                        }
                    }
                } else
                {
                    transform.speed = 0;
                }
            }
        }
    }
}
