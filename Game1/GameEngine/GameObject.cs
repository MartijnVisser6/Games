using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameEngine
{
    public class GameObject
    {
        public Vector2 Position { get; set; }
      
        private Texture2D _mTexture;



        public GameObject(Texture2D texture, Vector2 position)
        {
            _mTexture = texture;
            Position = position;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_mTexture, Position, Color.White);
        }

        public Rectangle GetBoundingBox()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, _mTexture.Width, _mTexture.Height);
        }

        public bool CheckCollision(GameObject obj)
        {
            if (this.GetBoundingBox().Intersects(obj.GetBoundingBox()))
                return true;

            return false;
        }
    }
}
