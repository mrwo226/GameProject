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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        string[] menuItems; // The array holding the menu options.
        int selectedIndex; // The option that is currently selected.

        Color normal = Color.White; // The default color for the text.
        Color highlight = Color.Turquoise;  // The color the text is when an option is selected.

        SpriteBatch spriteBatch; // Used for rendering the menu.
        SpriteFont spriteFont; // Holds data for the font used.

        // Where the menu is located on the game screen.
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        // The width of the menu.
        float width = 0f;
        public float Width
        {
            get { return width; }
        }

        // The height of the menu.
        float height = 0f;
        public float Height
        {
            get { return height; }
        }

        // Methods to get and set the selected option.  
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (selectedIndex < 0)
                    selectedIndex = 0;
                else if (selectedIndex >= menuItems.Length)
                    selectedIndex = menuItems.Length - 1;
            }
        }

        // The constructor for the menu component.
        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, string[] menuItems)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.menuItems = menuItems;
            MeasureMenu();
        }

        // This function determines the dimensions of the menu, namely the initial position of it's location.
        private void MeasureMenu()
        {
            // The height and width of the menu is set to zero by default.
            height = 0;
            width = 0;
            spriteFont.LineSpacing = 80; // The padding in between line of text is 80 pixels by default.

            // A for loop checks the width of every line of text in the menu.  It then sets the width of the menu to be equal to the longest line.
            foreach (string item in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += spriteFont.LineSpacing + 15;
            }

            // The start menu is located in the lower right hand corner, which is roughly one-fourth of the width of the game screen and roughly three-fourths
            // the height of the game screen.
            if (menuItems[0] == "New Game")
            {
                position = new Vector2((Game.Window.ClientBounds.Width - width) / 4 - 25,
                                        3 * (Game.Window.ClientBounds.Height - height) / 4 + 65);
            }

            // Horizontally, the pause menu is located exactly in the center of the screen.  Vertically, it is roughly in the center.
            else if (menuItems[0] == "Resume")
            {
                position = new Vector2((Game.Window.ClientBounds.Width - width) / 2,
                                        (Game.Window.ClientBounds.Height - height) / 2 + 85);
            }
            // The quit menus is located exactly in the center of the screen
            else if (menuItems[0] == "Yes")
            {
                // The menu is located roughly in the center of the screen.
                position = new Vector2((Game.Window.ClientBounds.Width - width) / 2 - 150,
                                        (Game.Window.ClientBounds.Height - height) / 2 + 125);
            }
            
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // If the player scrolls down, move to the next index.  If at the end of the array, move back to the beginning.
            if (InputManager.IsActionTriggered(InputManager.Action.CursorDown))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            // If the player scrolls up, move back to the last index.  If at the beginning of the array, move to the end.
            else if (InputManager.IsActionTriggered(InputManager.Action.CursorUp))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Vector2 location = position; // The location of the menu starts at the initial position.
            Color tint;

            // Draw every menu option on the screen.
            for (int i = 0; i < menuItems.Length; i++)
            {
                // If an option is selected, highlight it.
                if (i == selectedIndex)
                    tint = highlight;
                else
                    tint = normal;

                // If the menu is vertical, measure the width of the current text and center it.
                if (menuItems[0] != "Yes")
                {
                    Vector2 textDimension = spriteFont.MeasureString(menuItems[i]);
                    location.X = (float)(position.X + width / 2 - textDimension.X / 2);
                }

                // Draw the text to the screen and then update the vertical and/or horizontal position for the next line in the array.
                spriteBatch.DrawString(spriteFont, menuItems[i], location, tint);
                if (menuItems[0] == "New Game")
                    location.Y += spriteFont.LineSpacing + 5;
                else if (menuItems[0] == "Resume")
                    location.Y += spriteFont.LineSpacing + 12;
                else if (menuItems[0] == "Yes")
                    location.X += spriteFont.LineSpacing + 220;

            }
        }
    }
}
