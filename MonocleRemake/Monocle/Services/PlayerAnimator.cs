using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonocleRemake.Monocle.Services
{
    class PlayerAnimator : Service
    {
        private static Type[] QueryComponents = {
            typeof(PlayerTag),
        };

        public PlayerAnimator()
        {
            query = Query.All(QueryComponents);
        }

        override public void Execute(Entity[] entities, World w)
        {
            KeyboardState state = Keyboard.GetState();

            foreach (Entity entity in entities)
            {
                Animation anim = entity.GetComponent<Animation>();


                if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.A))
                {
                    if(anim.currentAction != "walking")
                    {
                        anim.currentFrame = 1;
                        anim.currentAction = "walking";
                    }
                }
                else if (state.IsKeyDown(Keys.Space))
                {
                    anim.currentFrame = 0;
                    anim.currentTimer = 101;
                    anim.currentAction = "casting";
                }
                else
                {
                    anim.currentAction = "standing";
                    anim.currentFrame = 0;
                }
                
            }
        }
    }
}
