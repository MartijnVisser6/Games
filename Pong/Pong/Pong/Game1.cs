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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentLoader.Content = Content;
            player1 = new Bat(new Vector2(0, 300));
            player2 = new Bat(new Vector2(768, 300));
        }
      
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            player1.Update(gameTime);
            player2.Update(gameTime);
            MoveBats();
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
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

            if (ks.IsKeyDown(Keys.Up))
            {
                player2.Position += new Vector2(0, -3);
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                player2.Position += new Vector2(0, 3);
            }
        }
    }
}
