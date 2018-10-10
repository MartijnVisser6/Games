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
        Enemy enemy;
        Dictionary<string, Scene> sceneDic = new Dictionary<string, Scene>();
        PlayerHealth playerHealth;
        string activeScene;

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
            activeScene = "lvl1";
            ContentLoader.Content = Content;

            LevelImporter importer = new LevelImporter();
            Scene scene1 = importer.Import("../../../lvl1.txt");
            scene1.Gravity = new Vector2(0, 0.1f);
            player = new Player(importer.StartLocation);
            scene1.Add(player);
            finishPoint = new GameObject(ContentLoader.LoadSprite("wouterv1"), new Vector2(1500, 850))
            {
                ObjectType = GameEngine.ObjectType.Dynamic,
            };
            scene1.Add(finishPoint);
            sceneDic.Add("lvl1", scene1);

            //UI
            Texture2D SimpleTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            SimpleTexture.SetData(new[] { Color.White });
            playerHealth = new PlayerHealth(new Vector2(0, 0), player, SimpleTexture);

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

            sceneDic[activeScene].Update(gameTime);

            if (CheckWin()) activeScene = "lvl2";
            if (!CheckAlive()) Exit();
            playerHealth.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin();
            sceneDic[activeScene].Draw(spriteBatch);
            playerHealth.Draw(spriteBatch);
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

        private bool CheckAlive()
        {
            if (player.alive == false)
            {
                Console.WriteLine("DEAD");
                return false;
            }
            return true;
        }
    }
}
