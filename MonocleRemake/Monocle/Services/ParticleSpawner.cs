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

        

        public override void Execute(Entity[] entities, World w)
        {
            foreach(Entity entity in entities)
            {
                Emiter emit = entity.GetComponent<Emiter>();
                // should I spawn a particle?
                double pps = 1000 / emit.particlesPerSecond;
                 
                TimeSpan difference = DateTime.Now.Subtract(emit.lastEmit);

                if (emit.lastEmit.AddMilliseconds(pps).CompareTo(DateTime.Now) < 0)
                {
                    double missedParticles = difference.TotalMilliseconds / pps;
                    for(int i = 0; i < (int) missedParticles; i++)
                    {
                        Particle.Register(w, entity, emit.sprite);
                    }

                    emit.lastEmit = DateTime.Now;
                }
            }
        }
    }
}
