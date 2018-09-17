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
        GameObject finishPoint;

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
            player = new Player(new Vector2(0, 900));
            platforms = new List<Platform>();
            finishPoint = new GameObject(ContentLoader.LoadSprite("wouterv1"), new Vector2(1500, 850));




            platforms.Add(new Platform(new Vector2(300, 768)));
            platforms.Add(new Platform(new Vector2(300, 800)));
            platforms.Add(new Platform(new Vector2(400, 800)));
            platforms.Add(new Platform(new Vector2(432, 800)));
            platforms.Add(new Platform(new Vector2(480, 800)));

            platforms.Add(new Platform(new Vector2(400, 700)));
            platforms.Add(new Platform(new Vector2(432, 700)));
            platforms.Add(new Platform(new Vector2(464, 668)));
            platforms.Add(new Platform(new Vector2(464, 700)));
            
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
            if (CheckWin()) Console.WriteLine("You win..");

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
            finishPoint.Draw(spriteBatch);
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
            {
                if(!player.CanPlayerJump)
                    player.Velocity += new Vector2(0, 0.1f);
            }
                
        }

        //Collisions between player and platforms.
        private void HandleCollisions()
        {          
            player.CanPlayerJump = false;

            foreach (Platform p in platforms)
            {
                Rectangle playerBoundingbox = player.GetBoundingBox();
                Rectangle platformBoundingbox = p.GetBoundingBox();
                if (playerBoundingbox.Intersects(platformBoundingbox))
                {     
                    Vector2 positionVector = player.PreviousPosition - player.Position;
                    float xDiff = 0f;
                    float yDiff = 0f;
                    float playerBoundingBoxX = 0, playerBoundingBoxY = 0;
                    float platformBoundingBoxX = 0, platformBoundingBoxY = 0;

                    if (positionVector.X > 0)
                    {
                        playerBoundingBoxX = playerBoundingbox.Left;
                        platformBoundingBoxX = platformBoundingbox.Right;
                    }
                    else if (positionVector.X < 0)
                    {
                        playerBoundingBoxX = playerBoundingbox.Right;
                        platformBoundingBoxX = platformBoundingbox.Left;
                    }

                    if (positionVector.Y > 0)
                    {
                        playerBoundingBoxY = playerBoundingbox.Top;
                        platformBoundingBoxY = platformBoundingbox.Bottom;
                    }
                    else if (positionVector.Y < 0)
                    {
                        playerBoundingBoxY = playerBoundingbox.Bottom;
                        platformBoundingBoxY = platformBoundingbox.Top;
                    }

                    xDiff = platformBoundingBoxX - playerBoundingBoxX;
                    yDiff = platformBoundingBoxY - playerBoundingBoxY;

                    float xDiv = float.MaxValue;
                    float yDiv = float.MaxValue;

                    if(xDiff != 0)
                        xDiv = xDiff / positionVector.X;
                    if(yDiff != 0)
                        yDiv = yDiff / positionVector.Y;

                    if (xDiv < yDiv)
                    {
                        if(xDiff < 0)
                            player.Position = new Vector2(platformBoundingbox.Left - player.Texture.Width, player.Position.Y);
                        else if(xDiff > 0)
                            player.Position = new Vector2((platformBoundingbox.Right), player.Position.Y);
                    }
                    else
                    {
                        if (yDiff < 0)
                            player.Position = new Vector2(player.Position.X, platformBoundingbox.Top - player.Texture.Height);
                        else if (yDiff > 0)
                            player.Position = new Vector2(player.Position.X, platformBoundingbox.Bottom);
                    }
                } 
            }


            foreach (Platform p in platforms)
            {
                Rectangle platformBoundingbox = p.GetBoundingBox();

                if (player.Velocity.Y >= 0)
                {
                    Rectangle playerBoundingBoxJump = player.GetBoundingBox();
                    playerBoundingBoxJump.Offset(0, 1);
                    if (playerBoundingBoxJump.Intersects(platformBoundingbox))
                        player.CanPlayerJump = true;
                }

                if (player.Velocity.Y > 0)
                {
                    Rectangle playerBoundingBoxDown = player.GetBoundingBox();
                    playerBoundingBoxDown.Offset(0, 1);
                    if (playerBoundingBoxDown.Intersects(platformBoundingbox))
                        player.Velocity = new Vector2(0, 0);
                }
                else if (player.Velocity.Y < 0)
                {
                    Rectangle playerBoundingBoxUp = player.GetBoundingBox();
                    playerBoundingBoxUp.Offset(0, -1);
                    if (playerBoundingBoxUp.Intersects(platformBoundingbox))
                        player.Velocity = new Vector2(0, 0);
                }
            }
        }

        private bool CheckWin()
        {
            Rectangle playerBoundingbox = player.GetBoundingBox();
            Rectangle winCheck = finishPoint.GetBoundingBox();

            if (playerBoundingbox.Intersects(winCheck))
            {
                return true;
            }
            return false;
        }
    }
}
