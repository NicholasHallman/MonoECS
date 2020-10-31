using ECS;
using ECS.Monocle;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonocleRemake.Monocle.Services
{
    class LifetimeDespawner : Service
    {
        static Type[] typeComponents = new Type[] { typeof(Lifetime) };

        public LifetimeDespawner()
        {
            query = new Query(typeComponents);
        }

        public override void Execute(Entity[] entities, World w)
        {
            foreach(Entity entity in entities){
                Lifetime lt = entity.GetComponent<Lifetime>();
                int difference = DateTime.Now.CompareTo(lt.birth.AddMilliseconds(lt.lifeInMilliseconds));
                if (difference > 0)
                {
                    w.RemoveEntity(entity);
                }
            }
        }
    }
}
