using GameEngine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoutersRevenge
{
    public class Player : GameObject
    {
        public Player(Vector2 position) : base(ContentLoader.LoadSprite("wouterv1"), position)
        {

        }
    }
}
