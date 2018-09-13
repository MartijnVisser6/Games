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

namespace WoutersRevenge
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        List<Platform> platforms;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = Globals.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = Globals.SCREEN_HEIGHT;
            graphics.ApplyChanges();
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

            ContentLoader.Content = Content;
            player = new Player(new Vector2(0, 0));
            platforms = new List<Platform>();

            platforms.Add(new Platform(new Vector2(300, 800)));
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

            // TODO: Add your update logic here
            player.Update(gameTime);

            //Update gravity
            UpdateGravity();
            HandleCollisions();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            foreach (Platform p in platforms)
                p.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void UpdateGravity()
        {
            if (player.Position.Y >= 900 - 32)
            {
                player.Position = new Vector2(player.Position.X, 900 - 32);
                player.Velocity = new Vector2(0, 0);
            }
            else
                player.Velocity += new Vector2(0, 0.1f);
        }

        private void HandleCollisions()
        {
            Rectangle playerBoundingbox = player.GetBoundingBox();

            foreach(Platform p in platforms)
            {
                if(p.CheckCollision(player))
                {
                    Rectangle platformBoundingbox = p.GetBoundingBox();

                    // Player is hitting the left side of the platform
                    if(playerBoundingbox.Left < platformBoundingbox.Left)
                    {
                        player.Position = new Vector2(platformBoundingbox.Left - playerBoundingbox.Width, player.Position.Y);
                    }
                    else if (playerBoundingbox.Right > platformBoundingbox.Right)
                    {
                        player.Position = new Vector2(platformBoundingbox.Right, player.Position.Y);
                    }

                    playerBoundingbox = player.GetBoundingBox();

                    if (p.CheckCollision(player))
                    {
                        // Player is below platform
                        if (playerBoundingbox.Top > platformBoundingbox.Top)
                        {
                            player.Position = new Vector2(player.Position.X, platformBoundingbox.Bottom);
                            player.Velocity = new Vector2(0, 0);
                        }
                        // Player is above platform
                        else if (playerBoundingbox.Bottom < platformBoundingbox.Bottom)
                        {
                            player.Position = new Vector2(player.Position.X, platformBoundingbox.Top - (playerBoundingbox.Height - 1));
                            player.Velocity = new Vector2(0, 0);
                        }
                    }
                }
            }
        }
    }
}
