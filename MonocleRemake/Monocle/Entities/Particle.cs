using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Monocle
{
    class Particle
    {
        static public Entity Register(World w, Color c)
        {
            Entity particle = w.CreateEntity()
                .AddComponent<Position>()
                .AddComponent<Direction>()
                .AddComponent<Sprite>()
                .AddComponent<Lifetime>()
                .AddComponent<ParticleTag>();

            Sprite sprite = particle.GetComponent<Sprite>();
            Texture2D texture = new Texture2D(World.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { c });
            sprite.texture = texture;
            sprite.size = new Vector2(1, 1);

            return particle;
        }
    }
}
