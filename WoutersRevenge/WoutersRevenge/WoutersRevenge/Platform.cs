using GameEngine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoutersRevenge
{
    class Platform : GameObject
    {
        public Platform(Vector2 position) : base(ContentLoader.LoadSprite("platformBlock"), position)
        {
            ObjectType = GameEngine.ObjectType.Static;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
