using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ECS.Monocle
{
    class Particle
    {
        static private Vector2 polarToVector(int polar)
        {
            double radians = polar * Math.PI / 180;
            return new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
        }

        static public Entity Register(World w, Entity emitterEntity, Texture2D initTexture = null)
        {
            Emiter emit = emitterEntity.GetComponent<Emiter>();
            Transform emitTransform = emitterEntity.GetComponent<Transform>();

            Entity particle = w.CreateEntity()
                .AddComponent<Transform>()
                .AddComponent<Sprite>()
                .AddComponent<Lifetime>()
                .AddComponent<ParticleTag>();

            Sprite sprite = particle.GetComponent<Sprite>();
            Texture2D texture = initTexture != null ? initTexture :  emit.particleTexture;
            sprite.texture = texture;
            sprite.size = new Vector2(1, 1);
            sprite.scale = 4;


            Lifetime life = particle.GetComponent<Lifetime>();
            life.birth = DateTime.Now;
            life.lifeInMilliseconds = emit.particleLifeTime;

            Random rand = new Random();

            int randomPolarDirection = rand.Next(emit.direction, emit.direction + emit.spray);

            Transform transform = particle.GetComponent<Transform>();
            transform.direction = polarToVector(randomPolarDirection) + emitTransform.direction;
            transform.speed = emit.particleSpeed;
            if (emit.type == EmiterType.Point)
            {
                transform.position = new Vector2(emitTransform.position.X, emitTransform.position.Y);
            } else if (emit.type == EmiterType.Sphere)
            {
                int radius = rand.Next(0, emit.radius);
                float xComp = (float)(rand.NextDouble() * 2) - 1;
                float yComp = (float)(rand.NextDouble() * 2) - 1;
                Vector2 rotation = new Vector2( xComp * radius, yComp * radius);
                transform.position = new Vector2(emitTransform.position.X + rotation.X, emitTransform.position.Y + rotation.Y);
            }

            return particle;
        }
    }
}
