using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonocleRemake.Monocle.Services
{
    class DirectionMover : Service
    {
        static Type[] componentTypes = new Type[] { typeof(Direction), typeof(Position) };

        public DirectionMover()
        {
            query = new Query(componentTypes);
        }

        public override void Execute(Entity[] entities, World w)
        {
            Parallel.ForEach(entities, (entity) =>
                {
                    Position position = entity.GetComponent<Position>();
                    Direction direction = entity.GetComponent<Direction>();

                    position.position = new Vector2(
                        position.position.X + (direction.direction.X * direction.speed),
                        position.position.Y + (direction.direction.Y * direction.speed)
                    );
                }
            );
        }

        private async Task ExecuteGroup(Entity[] entiites, int start, int end)
        {
            
        }
    }
}
