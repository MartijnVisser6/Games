using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoutersRevenge
{
    public class Player : GameObject
    {
        KeyboardState previousKs;       

        public Player(Vector2 position) : base(ContentLoader.LoadSprite("BiggerWouter"), position)
        {
            previousKs = Keyboard.GetState();
            ObjectType = GameEngine.ObjectType.Dynamic;
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            CheckCollisionWithEnemy();
            base.Update(gameTime);
        }

        public void HandleInput()
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.A))
            {
                this.Position += new Vector2(-Globals.PLAYER_SPEED, 0);
            }
            if (ks.IsKeyDown(Keys.D))
            {
                this.Position += new Vector2(Globals.PLAYER_SPEED, 0);
            }

            if (ks.IsKeyDown(Keys.W) && !previousKs.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Space) && !previousKs.IsKeyDown(Keys.Space))
            {
                if(CanJump())
                    this.Velocity += new Vector2(0, -Globals.PLAYER_JUMP);
            }

            previousKs = ks;
        }

        private bool CanJump()
        {            
            return this.IsOnGround;
        }

        private void CheckCollisionWithEnemy()
        {
            foreach(GameObject obj in Collisions)
            {
                if(obj is Enemy)
                {
                    Console.WriteLine("Hit enemy!");
                }
            }
                
        }



    }
}
