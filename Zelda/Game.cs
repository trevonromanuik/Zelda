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

using Zelda.Screens;

namespace Zelda
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //private InputManager _inputManager;
        private ScreenManager _screenManager;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 576;
            graphics.PreferMultiSampling = false;

            Content.RootDirectory = "Content";

            //_inputManager = new InputManager();
            _screenManager = new ScreenManager();
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
            AssetManager.Initialize(Content);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _screenManager.LoadNewScreen(new GameScreen(_screenManager));

            // TODO: use this.Content to load your game content here
            //RoomManager.Instance.LoadContent(Content);
            //Player.Instance.LoadContent(Content);

            //new Room(Color.White).LoadContent(Content);
            //new Bomb(0, 0).LoadContent(Content);
            //new Explosion(0, 0).LoadContent(Content);
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            //_inputManager.Update(gameTime.ElapsedGameTime.TotalSeconds);
            _screenManager.Update(gameTime.ElapsedGameTime.TotalSeconds);

            // TODO: Add your update logic here
            //RoomManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(196, 207, 161));

            //Draw to the Render Target
            RenderTarget2D target = new RenderTarget2D(GraphicsDevice, 160, 144);
            GraphicsDevice.SetRenderTarget(target);

            //RoomManager.Instance.Draw(gameTime, spriteBatch);
            spriteBatch.Begin();
            _screenManager.Draw(spriteBatch);
            spriteBatch.End();

            //Draw to the Back Buffer
            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            spriteBatch.Draw(target, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
