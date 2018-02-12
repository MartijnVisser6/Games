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
using GameEngine;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Bat player1, player2;
        List<Ball> balls;
        Random random;
        SpriteFont font;
        int player1Score, player2Score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            random = new Random();
        }

        protected override void Initialize()
        {           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentLoader.Content = Content;
            player1 = new Bat(new Vector2(0, 176));
            player2 = new Bat(new Vector2(768, 176));
            balls = new List<Ball>();
            for (int i = 0; i < 10; i++)
            {
                balls.Add(new Ball(new Vector2(392, 232)));
            }
            font = Content.Load<SpriteFont>("font");
            foreach (Ball ball in balls)
                BallReset(ball);
        }
      
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            player1.Update(gameTime);
            player2.Update(gameTime);
            MoveBats();

            foreach(Ball ball in balls)
                ball.Update(gameTime);

            MoveBall();
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);

            foreach (Ball ball in balls)
                ball.Draw(spriteBatch);

            DrawScore();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void MoveBats()
        {
            KeyboardState ks = Keyboard.GetState();
            if(ks.IsKeyDown(Keys.W))
            {
                player1.Position += new Vector2(0, -3);
            }
            if (ks.IsKeyDown(Keys.S))
            {
                player1.Position += new Vector2(0, 3);
            }

            if(player1.Position.Y < 0)
            {
                player1.Position += new Vector2(0, -player1.Position.Y);
            }
            else if(player1.Position.Y > 352)
            {
                player1.Position += new Vector2(0, 352 - player1.Position.Y);
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                player2.Position += new Vector2(0, -3);
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                player2.Position += new Vector2(0, 3);
            }

            if (player2.Position.Y < 0)
            {
                player2.Position += new Vector2(0, -player2.Position.Y);
            }
            else if (player2.Position.Y > 352)
            {
                player2.Position += new Vector2(0, 352 - player2.Position.Y);
            }


        }

        private void MoveBall()
        {
            foreach (Ball ball in balls)
            {
                if (ball.Position.Y <= 0 || ball.Position.Y >= 464)
                {
                    ball.Velocity *= new Vector2(1, -1);
                }

                if (ball.CheckCollision(player1)) 
                {
                    ball.Velocity *= new Vector2(-1.1f, 1.1f);
                    float yDiff = ball.Position.Y - player1.Position.Y - 64;
                    ball.Velocity += new Vector2(0, yDiff / 32.0f);
                }

                if(ball.CheckCollision(player2))
                {
                    ball.Velocity *= new Vector2(-1.1f, 1.1f);
                    float yDiff = ball.Position.Y - player2.Position.Y - 64;
                    ball.Velocity += new Vector2(0, yDiff / 32.0f);
                }

                if (ball.Position.X <= -16)
                {
                    BallReset(ball);
                    player2Score++;

                }
                else if (ball.Position.X >= 800)
                {
                    BallReset(ball);
                    player1Score++;
                }
            }
        }

        private void BallReset(Ball ball)
        {
            ball.Position = new Vector2(392, 232);
            ball.Velocity = new Vector2((float)((random.Next(0,2) - 0.5) * 4), (float)(random.NextDouble() - 0.5) * 4);
        }

        private void DrawScore()
        {
            spriteBatch.DrawString(font, player1Score.ToString(), player1.Position + new Vector2(50, 56), Color.White);
            spriteBatch.DrawString(font, player2Score.ToString(), player2.Position + new Vector2(-50, 56), Color.White);
        }
    }
}
