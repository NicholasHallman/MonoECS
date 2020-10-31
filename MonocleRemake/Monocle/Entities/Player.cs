using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS.Monocle
{
    class Player
    {
        static public void Register(World w)
        {
            Entity playerEntity = w.CreateEntity()
                .AddComponent<Transform>()
                .AddComponent<Sprite>()
                .AddComponent<Animation>()
                .AddComponent<SpellLimiter>()
                .AddComponent<PlayerTag>()
                .AddComponent<Controller>();

            Transform transform = playerEntity.GetComponent<Transform>();
            transform.position = new Vector2(50, 50);
            transform.speed = 0;
            transform.direction = new Vector2(1, 0);

            Sprite sr = playerEntity.GetComponent<Sprite>();
            sr.texture = World.content.Load<Texture2D>("wizard_s_2");
            sr.size = new Vector2(16, 32);
            sr.rotation = 0;
            sr.scale = 4;

            Keyframes walking = new Keyframes
            {
                frames = new Texture2D[] {
                    World.content.Load<Texture2D>("wizard_s_1"),
                    World.content.Load<Texture2D>("wizard_s_2"),
                    World.content.Load<Texture2D>("wizard_s_3"),
                    World.content.Load<Texture2D>("wizard_s_4")
                },
                delays = new int[] { 10, 3, 10, 3 }
            };
            Keyframes standing = new Keyframes
            {
                frames = new Texture2D[]
                {
                    World.content.Load<Texture2D>("wizard_s_2")
                },
                delays = new int[] { 0 }
            };
            Keyframes casting = new Keyframes
            {
                frames = new Texture2D[]
                {
                    World.content.Load<Texture2D>("wizard_s_5")
                },
                delays = new int[] { 100 }
            };

            Animation anim = playerEntity.GetComponent<Animation>();
            Dictionary<string, Keyframes> actions = new Dictionary<string, Keyframes>();
            actions.Add("walking", walking );
            actions.Add("standing", standing );
            actions.Add("casting", casting );
            anim.actionFrames = actions;
            anim.currentAction = "walking";
            anim.currentTimer = 0;
            anim.currentFrame = 0;

            playerEntity.GetComponent<SpellLimiter>().lastCast = DateTime.Now;
            playerEntity.GetComponent<SpellLimiter>().coolDownMilliseconds = 250;
        }  
    }
}
