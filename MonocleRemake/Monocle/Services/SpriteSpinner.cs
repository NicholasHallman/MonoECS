using ECS;
using ECS.Monocle;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonocleRemake.Monocle.Services
{
    class SpriteSpinner : Service
    {
        static Type[] componentTypes = new Type[] { typeof(Spin) };

        public SpriteSpinner()
        {
            query = new Query(componentTypes);
        }
        public override void Execute(Entity[] entities, World w)
        {
            foreach(Entity entity in entities)
            {
                Sprite s = entity.GetComponent<Sprite>();
                s.rotation += 0.2;
                if (s.rotation > 360) s.rotation = 0;
            }
        }
    }
}
