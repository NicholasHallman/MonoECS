using ECS;
using ECS.Monocle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonocleRemake.Monocle.Services
{
    class DamageFromFire : Service
    {
        static Type[] typeComponents = new Type[] { typeof(Flammable), typeof(Health) };

        public DamageFromFire()
        {
            query = Query.All(typeComponents);
        }

        public override void Execute(Entity[] entities, World w)
        {
            Parallel.ForEach(entities, (entity) =>
            {
                Health h = entity.GetComponent<Health>();
                Flammable f = entity.GetComponent<Flammable>();
                h.current -= f.damagePerUpdate;
            });
        }
    }
}
