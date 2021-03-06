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

namespace Game1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        bool winner;
        SpriteFont font;
        PlayerBase playerBase;
        List<Wall> walls = new List<Wall>();
        List<Bullet> bullets = new List<Bullet>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 1024;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            ContentLoader.Content = Content;
                    
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("SpriteFont1");
            player = new Player(new Vector2(560, 560));
            playerBase = new PlayerBase(new Vector2(512, 512));
            
            
            
            //square = Content.Load<Texture2D>("square");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            player.Update(gameTime,walls);

            foreach (Bullet bullet in bullets)
                bullet.Update(gameTime);

            HandleInput(gameTime, walls);

            //squarePosition += new Vector2(1, 0); moves square
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            player.Draw(spriteBatch);
            playerBase.Draw(spriteBatch);

            foreach (Bullet bullet in bullets)
                bullet.Draw(spriteBatch);

            if (winner)
                spriteBatch.DrawString(font, "You've won!", new Vector2(500, 500), Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void HandleInput(GameTime gameTime, List<Wall> walls)
        {
            int mvSpeed = 3;

            float multiplier = 60 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f; //Keeping gametime consistency for movement speed.

            KeyboardState ks = Keyboard.GetState();

            Vector2 prevPosition = player.Position;

            if (ks.IsKeyDown(Keys.W))
            {
                player.Position += new Vector2(0, -mvSpeed * multiplier);
            }
            if (ks.IsKeyDown(Keys.S))
            {
                player.Position += new Vector2(0, mvSpeed * multiplier);
            }           

            foreach (Wall wall in walls)
            {
                if (player.CheckCollision(playerBase))
                {
                    player.Position = prevPosition;
                    break;
                }
            }

            prevPosition = player.Position;

            if (ks.IsKeyDown(Keys.A))
            {
                player.Position += new Vector2(-mvSpeed * multiplier, 0);
            }
            if (ks.IsKeyDown(Keys.D))
            {
                player.Position += new Vector2(mvSpeed * multiplier, 0);
            }

            if (player.CheckCollision(playerBase))
            { 
                {
                    player.Position = prevPosition;

                }
            }

            // Handle shooting

            MouseState mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 direction = (new Vector2(mouseState.X, mouseState.Y) - player.Position);
                direction.Normalize();
                Bullet bullet = new Bullet(player.Position + new Vector2(8, 8));
                bullet.Velocity = direction;
                bullets.Add(bullet);
            }
        }


    }
}