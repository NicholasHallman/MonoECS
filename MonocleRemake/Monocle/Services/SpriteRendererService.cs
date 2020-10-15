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
        public static Type[] QueryComponents = new Type[]{typeof(SpriteRenderer), typeof(Position) };

        public override void Execute(Entity[] entities)
        {
            foreach(Entity entity in entities)
            {
                SpriteRenderer sr = entity.GetComponent<SpriteRenderer>();
                sr.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null,
                Matrix.CreateScale(10f));
                sr.spriteBatch.Draw(sr.sprite, new Rectangle(0, 0, 8, 8), Color.White);
                sr.spriteBatch.End();
            }
        }
    }
}
