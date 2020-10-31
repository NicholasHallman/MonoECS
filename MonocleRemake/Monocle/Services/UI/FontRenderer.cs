using ECS;
using ECS.Monocle;
using ECS.Monocle.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonocleRemake.Monocle.Services.UI
{
    class FontRenderer : Service
    {
        Type[] componentTypes = new Type[] { typeof(Label), typeof(Transform) };
        
        public FontRenderer()
        {
            query = new Query(componentTypes);
        }

        public override void Execute(Entity[] entities, World w)
        {
            World.spriteBatch.Begin();

            foreach (Entity entity in entities)
            {
                Label l = entity.GetComponent<Label>();
                Transform t = entity.GetComponent<Transform>();
                World.spriteBatch.DrawString(l.spriteFont, l.text, t.position, l.color);
            }

            World.spriteBatch.End();

        }
    }
}
