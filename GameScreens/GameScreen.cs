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


namespace WindowsGame1.GameScreens
{
    /// <summary>
    /// This is an abstract class that all game screens will inherit from.  It provides the main methods that all screens will have access to, Show() and Hide(),
    /// which allow the screen to enable or disable itself.  It also holds all components within the screen and updates and draws them.
    /// </summary>
    public abstract class GameScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Fields

        List<GameComponent> components = new List<GameComponent>();
        protected Game game;
        protected SpriteBatch spriteBatch;

        #endregion

        #region Properties

        public List<GameComponent> Components
        {
            get { return components; }
        }

        #endregion

        #region Constructor

        public GameScreen(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        #endregion

        #region Methods

        // This function activates the screen so that it is visible and the user can interact with it.
        public virtual void Show()
        {
            this.Visible = true;
            this.Enabled = true;
            foreach (GameComponent component in components)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }

        // This function deactivates the screen.
        public virtual void Hide()
        {
            this.Visible = false;
            this.Enabled = false;
            foreach (GameComponent component in components)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (GameComponent component in components)
                if (component.Enabled == true)
                    component.Update(gameTime);
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (GameComponent component in components)
                if (component is DrawableGameComponent && ((DrawableGameComponent)component).Visible) 
                    ((DrawableGameComponent)component).Draw(gameTime);
        }

        #endregion
    }
}
