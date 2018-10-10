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

namespace LevelEditor
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class LevelEditor : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int screenWidth = 1600;
        int screenHeight = 900;
        int gridSize = 32;
        string activeSprite;
        int activeSpriteIndex;
        Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();

        List<GameObject> gameObjects = new List<GameObject>();

        public LevelEditor()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            IsMouseVisible = true;
            Content.RootDirectory = "Content";
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

            textureDict.Add("platform", Content.Load<Texture2D>("platformBlock"));
            textureDict.Add("test", Content.Load<Texture2D>("Enemy1"));

            activeSprite = textureDict.Keys.First();
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
            InputHelper.Update();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            HandleInput();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            Texture2D SimpleTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            SimpleTexture.SetData(new[] { Color.White });
            spriteBatch.Begin();

            for(int i = 0; i< screenWidth; i += gridSize)
                spriteBatch.Draw(SimpleTexture, new Rectangle(i, 0, 1, screenHeight), Color.Black);

            for (int i = 0; i < screenHeight; i += gridSize)
                spriteBatch.Draw(SimpleTexture, new Rectangle(0, i, screenWidth, 1), Color.Black);

            foreach(GameObject obj in gameObjects)
            {
                obj.Draw(spriteBatch);
            }

            spriteBatch.Draw(textureDict[activeSprite], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);


            spriteBatch.End();

            base.Draw(gameTime);
        }       
        
        private void HandleInput()
        {
            MouseState ms = Mouse.GetState();
            int x = ms.X;
            int y = ms.Y;

            if (InputHelper.IsLeftMouseButtonClicked())
            {
                int xg = x / gridSize;
                int yg = y / gridSize;

                gameObjects.Add(new GameObject(textureDict[activeSprite], new Vector2(xg * gridSize, yg * gridSize)));
            }

            if (InputHelper.IsRightMouseButtonClicked())
            {
                List<GameObject> rem = new List<GameObject>();
                foreach(GameObject obj in gameObjects)
                {
                    if (obj.CheckCollision(new Point(x, y)))
                        rem.Add(obj);
                }

                foreach (GameObject obj in rem)
                {
                    gameObjects.Remove(obj);
                }
            }

            if (InputHelper.IsButtonReleased(Keys.Up))
            {
                activeSpriteIndex++;

                if (activeSpriteIndex >= textureDict.Count)
                    activeSpriteIndex = 0;                
            }

            if (InputHelper.IsButtonReleased(Keys.Down))
            {
                activeSpriteIndex--;

                if (activeSpriteIndex < 0)
                    activeSpriteIndex = textureDict.Count - 1;
            }

            activeSprite = textureDict.Keys.ElementAt(activeSpriteIndex);
        } 

    }
}
