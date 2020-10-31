using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonocleRemake.Monocle.Services
{
    class ParticleMover : Service
    {
        static Type[] QueryComponents = { typeof(ParticleTag) };

        public ParticleMover(){
            query = Query.All(QueryComponents);
        }

        public override void Execute(Entity[] entities, World w)
        {

            for (int i = 1; i < entities.Length; i++)
            {
                Entity entity = entities[i];

                Transform transform = entity.GetComponent<Transform>();

                transform.position = new Vector2(transform.position.X + transform.direction.X, transform.position.Y + transform.direction.Y);

                if (transform.position.X >= 1920 || transform.position.X <= 0)
                {
                    transform.direction = new Vector2(transform.direction.X * -1, transform.direction.Y);
                }
                if (transform.position.Y >= 1080 || transform.position.Y <= 0)
                {
                    transform.direction = new Vector2(transform.direction.X, transform.direction.Y * -1);
                }
            }
        }
    }
}
