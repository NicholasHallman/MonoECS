using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonocleRemake.Monocle.Services
{
    class SpriteRendererService : Service
    {
        public static Type[] QueryComponents = new Type[]{
            typeof(Sprite), 
            typeof(Transform)
        };

        public SpriteRendererService()
        {
            query = Query.All(QueryComponents);
        }

        public override void Execute(Entity[] entities, World w)
        {
            World.spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp);

            foreach (Entity entity in entities)
            {
                Sprite sr = entity.GetComponent<Sprite>();
                Vector2 pos = (entity.GetComponent<Transform>()).position;
                SpriteEffects effect = sr.flipX ? SpriteEffects.FlipHorizontally : sr.flipX ? SpriteEffects.FlipVertically : SpriteEffects.None;
                Vector2 origin = new Vector2() { X = sr.size.X / 2, Y = sr.size.Y / 2 };
                World.spriteBatch.Draw(sr.texture, pos, null, Color.White, (float)sr.rotation, origin, sr.scale, effect, 1);
            }
            World.spriteBatch.End();
        }
    }
}
