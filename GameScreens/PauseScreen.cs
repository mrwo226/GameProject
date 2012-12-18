using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1.GameScreens
{
    /// <summary>
    /// This is a class that inherits from the base class GameScreen to provide a pause menu for the user.  It draws itself on top of the action screen, which
    /// renders but does not update.  The pause menu holds four options: Resume, Player Stats, Save, and Return to Main Menu.
    /// </summary>
    class PauseScreen : GameScreen
    {
        #region Fields

        MenuComponent menuComponent; // Provides the selection menu.
        Texture2D image; // Holds the image used for the menu.
        Rectangle imageRectangle; // Holds the size and location of the menu.
        ActionScreen actionScreen; // The screen the pause menu appears on top of.

        #endregion

        #region Properties

        // Returns the option the user has selected.
        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }

        #endregion

        #region Constructor

        public PauseScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D image, ActionScreen actionScreen)
            : base(game, spriteBatch)
        {
            // Initialize the menu options and add them to the menu component.
            string[] menuItems = { "Resume", "Player Stats", "Save", "Return to Main Menu" };
            menuComponent = new MenuComponent(game, spriteBatch, spriteFont, menuItems);
            Components.Add(menuComponent);

            // Set the image and calculate its size.
            this.image = image;
            imageRectangle = new Rectangle((Game.Window.ClientBounds.Width - this.image.Width) / 2, (Game.Window.ClientBounds.Height - this.image.Height) / 2,
                                            this.image.Width, this.image.Height);

            this.actionScreen = actionScreen;
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            // Render the action screen underneath the pause menu.
            actionScreen.Draw(gameTime);
            spriteBatch.Draw(image, imageRectangle, Color.White);
            base.Draw(gameTime);
        }

        #endregion
    }
}
