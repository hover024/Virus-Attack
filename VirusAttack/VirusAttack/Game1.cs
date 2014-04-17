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

namespace VirusAttack
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;
        private Level _currentLevel;
        private WaveController _waveController;
        private Player player;
        private Toolbar toolbar;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Tower Defence: Virus Attack 1.0";
            Window.AllowUserResizing = true;
            graphics.PreferMultiSampling = true;
            graphics.PreferredBackBufferWidth = Level.LevelWidth;
            graphics.PreferredBackBufferHeight = Level.LevelHeight + Level.BlockSize;
            IsMouseVisible = true;
            graphics.IsFullScreen = true;
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
            _currentLevel = LoadLevel(1);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _waveController = new WaveController(_currentLevel, 24, Content.Load<Texture2D>("enemy"));
            var font = Content.Load<SpriteFont>("GUIFont");
            toolbar = new Toolbar(Content.Load<Texture2D>("toolbar"), font,
                new Vector2(0, Level.LevelHeight));
            player = new Player(_currentLevel, Content.Load<Texture2D>("tower"), Content.Load<Texture2D>("bullet"));
            // TODO: use this.Content to load your game content here
        }

        protected Level LoadLevel(int levelNumber)
        {
            if (_currentLevel != null && _currentLevel.Number == levelNumber) return _currentLevel;
            var level = new Level();
            level.AddTexture(Content.Load<Texture2D>("Levels/Level" + levelNumber + "/ground"));
            level.AddTexture(Content.Load<Texture2D>("Levels/Level" + levelNumber + "/road"));
            level.LoadMap("Content/Levels/Level" + levelNumber + "/levelMap.txt");
            level.LoadWayPoints("Content/Levels/Level" + levelNumber + "/WayPoints.txt");
            return level;
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonStatus.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            player.Update(gameTime, _waveController.Enemies);
            _waveController.Update(gameTime);
            //enemy.CurrentHealth-=0.05f;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _currentLevel.Draw(_spriteBatch);
            _spriteBatch.Begin();
            _waveController.Draw(_spriteBatch);
            player.Draw(_spriteBatch);
            toolbar.Draw(_spriteBatch, player);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
