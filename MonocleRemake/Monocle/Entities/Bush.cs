using ECS;
using ECS.Monocle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Vortice.Direct3D12.Shader;

namespace MonocleRemake.Monocle.Entities
{
    class Bush
    {
        static public void Register(World w, Vector2 initPosiiton)
        {
            Entity e = w.CreateEntity()
                .AddComponent<Sprite>()
                .AddComponent<Flammable>()
                .AddComponent<Health>()
                .AddComponent<Transform>();

            Transform t = e.GetComponent<Transform>();
            t.position = initPosiiton;

            Sprite s = e.GetComponent<Sprite>();
            s.texture = World.content.Load<Texture2D>("bush");
            s.size = new Vector2(16, 16);
            s.scale = 4;

            Flammable flammable = e.GetComponent<Flammable>();
            flammable.isOnFire = false;
            flammable.percentChanceToSpread = 10;

            Health health = e.GetComponent<Health>();
            health.current = 100;
            health.total = 100;
        }
    }
}
