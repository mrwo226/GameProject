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
using XnaActionLibrary;

namespace WindowsGame1
{
    /// <summary>   
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameScreen activeScreen; // Holds the current screen.
        StartScreen startScreen; // The start menu.
        ActionScreen actionScreen; // The screen where most of the gameplay occurs.
        SplashScreen splashScreen; // The loading screen.
        PauseScreen pauseScreen; // The pause menu.
        QuitScreen quitScreen; // The message asking if the user is sure he/she wants to quit.
        Texture2D tilesetTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            // The preferred resolution is 1280x720.
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
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
            InputManager.Initialize();

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

            tilesetTexture = new Texture2D(GraphicsDevice, 256, 256);
            tilesetTexture = Content.Load<Texture2D>("Tilesets/tileset1");
            
            // Load the content for the start screen, add it to the components, and hide it.
            startScreen = new StartScreen(this, spriteBatch, Content.Load<SpriteFont>("menufont"), Content.Load<Texture2D>("Menus/StartScreen"));
            Components.Add(startScreen);
            startScreen.Hide();

            // Load the content for the loading screen.
            splashScreen = new SplashScreen(this, spriteBatch, Content.Load<Texture2D>("Menus/SampleSplashScreen"));
            Components.Add(splashScreen);
            splashScreen.Hide();

            // Load the content for the action screen, where most of the gameplay will occur.
            actionScreen = new ActionScreen(this, spriteBatch, tilesetTexture);
            Components.Add(actionScreen);
            actionScreen.Hide();

            // Load the content for the pause screen.
            pauseScreen = new PauseScreen(this, spriteBatch, Content.Load<SpriteFont>("menufont"), Content.Load<Texture2D>("Menus/PauseScreen"));
            Components.Add(pauseScreen);
            pauseScreen.Hide();

            // Load the content for the quit screen.
            quitScreen = new QuitScreen(this, spriteBatch, Content.Load<SpriteFont>("menufont"), Content.Load<Texture2D>("Menus/QuitScreen"));
            Components.Add(quitScreen);
            quitScreen.Hide();

            // When the game starts, the active screen is the start screen.
            activeScreen = startScreen;
            activeScreen.Show();
            
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
            
            // Update the input.
            InputManager.Update();

            // Connects to functions that handle whichever screen is active.
            if (activeScreen == startScreen)
            {
               HandleStartScreen();
            }

            else if (activeScreen == actionScreen)
            {
               HandleActionScreen();
            }

            else if (activeScreen == pauseScreen)
            {
               HandlePauseScreen();
            }

            else if (activeScreen == quitScreen)
            {
                HandleQuitScreen();
            }

            base.Update(gameTime);
        }

        // This function handles the start screen.
        private void HandleStartScreen()
        {
            // If the user selects the first option (New Game), then the action screen is loaded.  If the second option is selected, the load game screen
            // is activated (not yet implemented).  If the third option is selected, the game exits.
            if (InputManager.IsActionTriggered(InputManager.Action.Ok))
            {
                if (startScreen.SelectedIndex == 0)
                {
                    activeScreen.Hide();
                    activeScreen = actionScreen;
                    activeScreen.Show();
                }
                //if (startScreen.SelectedIndex == 1)
                //{
                //    activeScreen.Hide();
                //    activeScreen = loadGameScreen;
                //    activeScreen.Show();
                //}
                if (startScreen.SelectedIndex == 2)
                    this.Exit();
            }
        }

        // This function handles the action screen.
        private void HandleActionScreen()
        {
            // If the user presses escape, the pause menu is activated.
            if (InputManager.IsActionTriggered(InputManager.Action.Pause))
            {
                activeScreen.Hide();
                activeScreen = pauseScreen;
                activeScreen.Show();
            }
        }

        // This function handles the pause screen.
        private void HandlePauseScreen()
        {
            if (InputManager.IsActionTriggered(InputManager.Action.Ok))
            {
                // Resumes the game.
                if (pauseScreen.SelectedIndex == 0)
                {
                    activeScreen.Hide();
                    activeScreen = actionScreen;
                    activeScreen.Show();
                }
                // Activates the stats screen so the user can see items, abilities, etc.  (Not implemented)
                //if (pauseScreen.SelectedIndex == 1)
                //{
                //    activeScreen.Hide();
                //    activeScreen = statsScreen;
                //    activeScreen.Show();
                //}
                // Activates the save screen so the user can save the game. (Not implemented)
                //if (pauseScreen.SelectedIndex == 2)
                //{
                //    activeScreen.Hide();
                //    activeScreen = saveScreen;
                //    activeScreen.Show();
                //}
                // Initializes the quit screen.
                else if (pauseScreen.SelectedIndex == 3)
                {
                    activeScreen.Hide();
                    activeScreen = quitScreen;
                    activeScreen.Show();
                }
            }
        }

        private void HandleQuitScreen()
        {
            if (InputManager.IsActionTriggered(InputManager.Action.Ok))
            {
                // Exits to the main menu.
                if (quitScreen.SelectedIndex == 0)
                {
                    activeScreen.Hide();
                    activeScreen = startScreen;
                    activeScreen.Show();
                }
                // Goes back to the pause menu
                if (quitScreen.SelectedIndex == 1)
                {
                    activeScreen.Hide();
                    activeScreen = pauseScreen;
                    activeScreen.Show();
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.Identity);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
