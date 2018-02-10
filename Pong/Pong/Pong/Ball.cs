using GameEngine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    public class Ball : GameObject
    {
        public Ball(Vector2 position) : base(ContentLoader.LoadSprite("ball"), position)
        {

        }
    }
}
