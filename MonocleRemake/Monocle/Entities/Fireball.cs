using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ECS.Monocle
{
    class Fireball
    {
        public static void AddFireEmitter(Entity e)
        {
            e.AddComponent<Emiter>();

            Emiter emiter = e.GetComponent<Emiter>();
            emiter.particleLifeTime = 400;
            emiter.particlesPerSecond = 500;
            emiter.particleSpeed = 2;
            emiter.spray = 90;
            emiter.direction = -135;
            emiter.startColor = Color.Red;
            emiter.lastEmit = DateTime.Now;
            emiter.type = EmiterType.Sphere;
            emiter.radius = 50;
            emiter.particleTexture = new Texture2D(World.GraphicsDevice, 1, 1);
            emiter.particleTexture.SetData(new Color[] { emiter.startColor });
        }
        public static Entity Register(World w, Vector2 initPosition, Vector2 direction)
        {
            Entity e = w.CreateEntity()
                .AddComponent<Lifetime>()
                .AddComponent<Transform>()
                .AddComponent<Sprite>()
                .AddComponent<Flammable>()
                .AddComponent<Spin>();

            Transform t = e.GetComponent<Transform>();
            t.position = initPosition + (direction * 32);
            t.position.Y += 16;
            t.direction = direction;
            t.speed = 10;
            e.GetComponent<Lifetime>().birth = DateTime.Now;
            e.GetComponent<Lifetime>().lifeInMilliseconds = 1000;

            Fireball.AddFireEmitter(e);

            e.GetComponent<Sprite>().texture = World.content.Load<Texture2D>("fire");
            e.GetComponent<Sprite>().size = new Vector2(16, 16);
            e.GetComponent<Sprite>().rotation = 0;
            e.GetComponent<Sprite>().scale = 4;

            e.GetComponent<Flammable>().isOnFire = true;
            e.GetComponent<Flammable>().percentChanceToSpread = 1000;
            return e;
        }
    }
}
