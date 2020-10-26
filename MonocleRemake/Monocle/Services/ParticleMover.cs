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
            query = new Query(QueryComponents);
        }

        public override void Execute(Entity[] entities, World w)
        {

            for (int i = 1; i < entities.Length; i++)
            {
                Entity entity = entities[i];

                Position pos = entity.GetComponent<Position>();
                Direction dir = entity.GetComponent<Direction>();

                pos.position = new Vector2(pos.position.X + dir.direction.X, pos.position.Y + dir.direction.Y);

                if (pos.position.X >= 1920 || pos.position.X <= 0)
                {
                    dir.direction = new Vector2(dir.direction.X * -1, dir.direction.Y);
                }
                if (pos.position.Y >= 1080 || pos.position.Y <= 0)
                {
                    dir.direction = new Vector2(dir.direction.X, dir.direction.Y * -1);
                }
            }
        }
    }
}
