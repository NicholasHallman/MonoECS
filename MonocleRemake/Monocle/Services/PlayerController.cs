using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

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
            const int speed = 4;
            KeyboardState state = Keyboard.GetState();

            foreach (Entity entity in entities)
            {
                Direction dir = entity.GetComponent<Direction>();
                Sprite sprite = entity.GetComponent<Sprite>();

                Vector2 newDir = new Vector2()
                {
                    X = state.IsKeyDown(Keys.D) && state.IsKeyDown(Keys.A) ? 0 : state.IsKeyDown(Keys.D) ? 1 : state.IsKeyDown(Keys.A) ?  - 1 : 0,
                    Y = state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.S) ? 0 : state.IsKeyDown(Keys.W) ? -1 : state.IsKeyDown(Keys.S) ? 1 : 0
                };

                if(Math.Abs(newDir.X) + Math.Abs(newDir.Y) == 2)
                {
                    newDir.X /= 1.5f;
                    newDir.Y /= 1.5f;
                }

                if( newDir.X != 0 || newDir.Y != 0)
                {
                    dir.direction = newDir;
                    dir.speed = 4;
                } else
                {
                    dir.speed = 0;
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
                    if(DateTime.Now.CompareTo(sl.lastCast.AddMilliseconds(sl.coolDownMilliseconds)) > 0)
                    {
                        Position playerPos = entity.GetComponent<Position>();
                        Entity fireball = Fireball.Register(w, playerPos.position, dir.direction);
                        fireball.GetComponent<Direction>().speed += dir.speed;
                        sl.lastCast = DateTime.Now;
                    }   
                }
            }
        }
    }
}
