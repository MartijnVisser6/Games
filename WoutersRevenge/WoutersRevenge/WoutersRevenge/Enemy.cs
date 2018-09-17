using GameEngine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoutersRevenge
{
    class Enemy : GameObject
    {
        public Enemy(Vector2 position) : base(ContentLoader.LoadSprite("Enemy1"), position)
        {
            ObjectType = GameEngine.ObjectType.Static;
        }

        private void MoveLeft()
        {
            Position -= new Vector2(.1f, 0);
        }

        public override void Update(GameTime gameTime)
        {
            MoveLeft();
            base.Update(gameTime);
        }

    }
}
