using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1.GameScreens
{
    /// <summary>
    /// This is a class that inherits from the base class GameScreen to provide a start menu for the user.  The start menu holds three options: New Game, 
    /// Load Game, and Exit Game.
    /// </summary>
    class StartScreen : GameScreen
    {
        MenuComponent menuComponent; // The menu component contains the options that the player can choose from.
        Texture2D image; // The image containing the background and the menu graphics.
        Rectangle imageRectangle; // The size and location of the screen on the game window.

        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }

        public StartScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D image)
            : base(game, spriteBatch)
        {
            // Add the menu options to the menu component, and then add the component to the list of components.
            string[] menuItems = { "New Game", "Load Game", "Exit Game" };
            menuComponent = new MenuComponent(game, spriteBatch, spriteFont, menuItems);
            Components.Add(menuComponent);
            this.image = image;
            // The start screen will take up the entire game window.
            imageRectangle = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(image, imageRectangle, Color.White);
            base.Draw(gameTime);
        }
    }
}
