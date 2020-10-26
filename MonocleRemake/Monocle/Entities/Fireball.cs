using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Monocle
{
    class Fireball
    {
        public static Entity Register(World w, Vector2 initPosition, Vector2 direction)
        {
            Entity e = w.CreateEntity()
                .AddComponent<Emiter>()
                .AddComponent<Direction>()
                .AddComponent<Lifetime>()
                .AddComponent<Position>();

            e.GetComponent<Position>().position = initPosition + (direction * 32);
            e.GetComponent<Direction>().direction = direction;
            e.GetComponent<Direction>().speed = 10;
            e.GetComponent<Lifetime>().birth = DateTime.Now;
            e.GetComponent<Lifetime>().lifeInMilliseconds = 1000;

            Emiter emiter = e.GetComponent<Emiter>();
            emiter.particleLifeTime = 400;
            emiter.particlesPerSecond = 500;
            emiter.particleSpeed = 2;
            emiter.spray = 360;
            emiter.direction = 0;
            emiter.color = Color.Red;
            emiter.lastEmit = DateTime.Now;

            return e;
        }
    }
}
