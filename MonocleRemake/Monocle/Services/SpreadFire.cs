using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonocleRemake.Monocle.Services
{
    class SpreadFire : Service
    {
        static Type[] typeComponents = new Type[] { typeof(Flammable) };

        public SpreadFire()
        {
            base.query = new Query(typeComponents);
        }
        public override void Execute(Entity[] entities, World w)
        {
            for(int i = 0; i < entities.Length; i++)
            {
                Entity first = entities[i];
                if (first.GetComponent<Flammable>().isOnFire)
                {
                    for (int j = 0; j < entities.Length; j++)
                    {
                        if (j != i)
                        {
                            Entity second = entities[j];
                            // roll the dice to see if this will catch on fire
                            Random rand = new Random();
                            bool willBurn = rand.Next(0, 1000) <= first.GetComponent<Flammable>().percentChanceToSpread;
                            if (!second.GetComponent<Flammable>().isOnFire && willBurn)
                            {
                                Transform firstT = first.GetComponent<Transform>();
                                Transform secondT = second.GetComponent<Transform>();
                                double diffX = Math.Pow(firstT.position.X - secondT.position.X, 2);
                                double diffY = Math.Pow(firstT.position.Y - secondT.position.Y, 2);
                                double distance = Math.Sqrt(diffX + diffY);
                                if (distance <= 64)
                                {
                                    second.GetComponent<Flammable>().isOnFire = true;

                                    second.AddComponent<Lifetime>();
                                    Lifetime lt = second.GetComponent<Lifetime>();
                                    lt.birth = DateTime.Now;
                                    lt.lifeInMilliseconds = 2000;

                                    Fireball.AddFireEmitter(second);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
