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
        public Vector2 PreviousPosition { get; set; }
        public Vector2 Velocity { get; set; }
        public Texture2D Texture { get; set; }        
        public ObjectType ObjectType { get; set; }
        public bool IsOnGround { get; set; }

        public GameObject(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            PreviousPosition = position;
        }

        public virtual void PreUpdate(GameTime gameTime)
        {
            PreviousPosition = Position;
        }

        public virtual void Update(GameTime gameTime)
        {            
            Position += Velocity * ((gameTime.ElapsedGameTime.Milliseconds / 1000.0f) * 60);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public Rectangle GetBoundingBox()
        {
            return new Rectangle((int)Math.Round(Position.X), (int)Math.Round(Position.Y), Texture.Width, Texture.Height);
        }

        public bool CheckCollision(GameObject obj)
        {
            if (this.GetBoundingBox().Intersects(obj.GetBoundingBox()))
                return true;

            return false;
        }
    }
}
