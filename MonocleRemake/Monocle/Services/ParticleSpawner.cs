using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonocleRemake.Monocle.Services
{
    class ParticleSpawner : Service
    {
        static Type[] componentTypes = new Type[] { typeof(Emiter) };

        public ParticleSpawner()
        {
            query = new Query(componentTypes);
        }

        private Vector2 polarToVector(int polar)
        {
            double radians = polar * Math.PI / 180;
            return new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
        }

        public override void Execute(Entity[] entities, World w)
        {
            foreach(Entity entity in entities)
            {
                Emiter emit = entity.GetComponent<Emiter>();
                Direction emitDir = entity.GetComponent<Direction>();
                // should I spawn a particle?
                double pps = 1000 / emit.particlesPerSecond;
                 
                TimeSpan difference = DateTime.Now.Subtract(emit.lastEmit);

                if (emit.lastEmit.AddMilliseconds(pps).CompareTo(DateTime.Now) < 0)
                {
                    double missedParticles = difference.TotalMilliseconds / pps;
                    for(int i = 0; i < (int) missedParticles; i++)
                    {
                        Entity particle = Particle.Register(w, emit.color);

                        Position emitterPosition = entity.GetComponent<Position>();

                        particle.GetComponent<Position>().position = new Vector2(emitterPosition.position.X, emitterPosition.position.Y);
                        Lifetime life = particle.GetComponent<Lifetime>();
                        life.birth = DateTime.Now;
                        life.lifeInMilliseconds = emit.particleLifeTime;

                        Random rand = new Random();

                        int randomPolarDirection = rand.Next(emit.direction, emit.direction + emit.spray);

                        Direction particleDir = particle.GetComponent<Direction>();
                        particleDir.direction = polarToVector(randomPolarDirection) + emitDir.direction;
                        particleDir.speed = emit.particleSpeed;
                    }

                    emit.lastEmit = DateTime.Now;
                }
            }
        }
    }
}
