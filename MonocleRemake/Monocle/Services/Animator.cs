using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework.Graphics;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonocleRemake.Monocle.Services
{
    class Animator: Service
    {
        static Type[] componentTypes = new Type[] { typeof(Animation), typeof(Sprite) };

        public Animator()
        {
            query = Query.All(componentTypes);
        }
        public override void Execute(Entity[] entities, World w)
        {
            foreach(Entity entitiy in entities)
            {
                Animation anim = entitiy.GetComponent<Animation>();
                Sprite sprite = entitiy.GetComponent<Sprite>();

                int currentFrame = anim.currentFrame;
                string action = anim.currentAction;
                Texture2D[] frames = anim.actionFrames[action].frames;
                int[] delays = anim.actionFrames[action].delays;

                if (currentFrame < delays.Length && anim.currentTimer > delays[currentFrame])
                {
                    anim.currentTimer = 0;
                    anim.currentFrame += 1;
                    if(anim.currentFrame >= frames.Length)
                    {
                        anim.currentFrame = 0;
                    }
                    sprite.texture = frames[anim.currentFrame];
                }
                anim.currentTimer += 1;
            }
        }
    }
}
