using ComputeSharp;
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
        static Type[] componentTypes = new Type[] { typeof(Transform) };

        public DirectionMover()
        {
            query = new Query(componentTypes);
        }

        public override void Execute(Entity[] entities, World w)
        {

            Parallel.ForEach(entities, (entity) =>
                {
                    Transform transform = entity.GetComponent<Transform>();

                    transform.position = new Vector2(
                        transform.position.X + (transform.direction.X * transform.speed),
                        transform.position.Y + (transform.direction.Y * transform.speed)
                    );
                }
            );
        }

        public readonly struct ExecuteShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> positionsX;
            public readonly ReadWriteBuffer<float> positionsY;
            public readonly ReadOnlyBuffer<float> directionsX;
            public readonly ReadOnlyBuffer<float> directionsY;
            public readonly ReadOnlyBuffer<float> speeds;


            public ExecuteShader(
                ReadOnlyBuffer<float> directionsX,
                ReadOnlyBuffer<float> directionsY,
                ReadWriteBuffer<float> positionsX,
                ReadWriteBuffer<float> positionsY,
                ReadOnlyBuffer<float> speeds)
            {
                this.directionsX = directionsX;
                this.directionsY = directionsY;
                this.positionsX = positionsX;
                this.positionsY = positionsY;
                this.speeds = speeds;
            }

            public void Execute(ThreadIds ids)
            {
                System.Numerics.Vector2 newPosition = new System.Numerics.Vector2();
                newPosition.X = positionsX[ids.X] + (directionsX[ids.X] * speeds[ids.X]);
                newPosition.Y = positionsY[ids.X] + (directionsY[ids.X] * speeds[ids.X]);
                positionsX[ids.X] = newPosition.X;
                positionsY[ids.X] = newPosition.Y;
            }
        }
    }
}
