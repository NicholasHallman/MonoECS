using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonocleRemake.Monocle.Entities
{
    class CherryPetalEmitter
    {

        static public Entity Register(World w)
        {
            Entity e = w.CreateEntity()
                .AddComponent<Emiter>()
                .AddComponent<Transform>();

            Transform t = e.GetComponent<Transform>();
            t.position = new Vector2( 1920 / 2, -1080);
            t.position.Y += 16;
            t.direction = new Vector2(0,0);
            t.speed = 0;

            Emiter emiter = e.GetComponent<Emiter>();
            emiter.particleLifeTime = 20000;
            emiter.particlesPerSecond = 4;
            emiter.particleSpeed = 2;
            emiter.spray = 0;
            emiter.direction = 90;
            emiter.startColor = Color.Red;
            emiter.lastEmit = DateTime.Now;
            emiter.type = EmiterType.Sphere;
            emiter.radius = 1080;
            emiter.sprite = World.content.Load<Texture2D>("cherry-petal");

            return e;
        }
    }
}
