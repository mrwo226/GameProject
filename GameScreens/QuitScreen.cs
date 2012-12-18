using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaActionLibrary;

namespace WindowsGame1.GameScreens
{
    /// <summary>
    /// This is a class that inherits from the base class GameScreen to provide a quit confirmation menu for the user.  It draws itself on top of the action screen, 
    /// which renders but does not update.  The pause menu holds two options, "Yes" and "No."
    /// </summary>
    class QuitScreen : GameScreen
    {
        #region Fields

        MenuComponent menuComponent;
        Texture2D image;
        Rectangle imageRectangle;
        ActionScreen actionScreen;

        #endregion

        #region Properties

        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }

        #endregion

        #region Constructor

        public QuitScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D image, ActionScreen actionScreen)
            : base(game, spriteBatch)
        {
            // Adds the two options to the menu component, which is then added to the list of components in GameScreen.
            string[] menuItems = { "Yes", "No" };
            menuComponent = new MenuComponent(game, spriteBatch, spriteFont, menuItems);
            Components.Add(menuComponent);
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
            // Renders the quit screen on top of the action screen.
            actionScreen.Draw(gameTime);
            spriteBatch.Draw(image, imageRectangle, Color.White);
            base.Draw(gameTime);
        }

        #endregion
    }
}
