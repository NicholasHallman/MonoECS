using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ECS;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS.Monocle
{
    class Player
    {
        static public void Register(World w, ContentManager Content, SpriteBatch spriteBatch)
        {
            Entity playerEntity = w.CreateEntity()
                .AddComponent<Position>()
                .AddComponent<SpriteRenderer>();

            SpriteRenderer sr = playerEntity.GetComponent<SpriteRenderer>();
            sr.sprite = Content.Load<Texture2D>("guy");
            sr.spriteBatch = spriteBatch;
        }  
    }
}
