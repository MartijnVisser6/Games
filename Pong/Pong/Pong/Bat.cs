using GameEngine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    public class Bat : GameObject
    {
        public Bat(Vector2 position) : base(ContentLoader.LoadSprite("bat"), position)
        {

        }
    }
}
