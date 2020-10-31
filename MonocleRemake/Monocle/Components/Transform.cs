using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ECS;
using Microsoft.Xna.Framework;

namespace ECS.Monocle
{
    class Transform : Component
    {
        public Vector2 position;
        public Vector2 direction;
        public float speed;
    }
}
