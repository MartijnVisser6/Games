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
        List<GameObject> platforms;
        GameObject finishPoint;
        Scene scene1;

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
            scene1 = new Scene()
            {
                Gravity = new Vector2(0f, 0.1f),
            };

            player = new Player(new Vector2(0, 700));
           
            platforms = new List<GameObject>();
            finishPoint = new GameObject(ContentLoader.LoadSprite("wouterv1"), new Vector2(1500, 850));
          

            for(int i = 0; i < 60; i++)
            {
                platforms.Add(new Platform(new Vector2(0 + 32 * i, 868)));
            }

            platforms.Add(new Platform(new Vector2(400, 868 - 32)));
            platforms.Add(new Platform(new Vector2(400, 868 - 64)));
            platforms.Add(new Platform(new Vector2(400, 868 - 96)));
            scene1.Add(player);
            scene1.Add(platforms);
            scene1.Add(new Enemy(new Vector2(500, 700)));
        }

        /// <summary>s
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            scene1.Update(gameTime);

            if (CheckWin()) Console.WriteLine("You win..");

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            foreach (Platform p in platforms)
                p.Draw(spriteBatch);
            finishPoint.Draw(spriteBatch);
            spriteBatch.End();           

            base.Draw(gameTime);
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
