using GameEngine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    public class Bullet : GameObject
    {
        public Bullet(Vector2 position) : base(ContentLoader.LoadSprite("Bullet"), position)
        {

        }
    }
}
