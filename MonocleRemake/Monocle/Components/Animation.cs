using ECS;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Monocle
{
    struct Keyframes
    {
        public Texture2D[] frames;
        public int[] delays;
    }
    class Animation : Component
    {
        public string currentAction;
        public int currentTimer;
        public int currentFrame;
        public Dictionary<string, Keyframes> actionFrames;
    }
}
