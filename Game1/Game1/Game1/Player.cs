using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Player : GameObject
    {
        public Player(Vector2 position) : base(ContentLoader.LoadSprite("square"), position)
        {
            
        }

        public void Update(GameTime gameTime, List<Wall> walls)
        {
            
            base.Update(gameTime);
        }

        
    }
}
