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


namespace WindowsGame1
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        string[] menuItems;
        int selectedIndex;

        Color normal = Color.White;
        Color highlight = Color.Turquoise;

        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        Vector2 position;
        float width = 0f;
        float height = 0f;

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

        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, string[] menuItems)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.menuItems = menuItems;
            MeasureMenu();
        }

        private void MeasureMenu()
        {
            height = 0;
            width = 0;

            foreach (string item in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += spriteFont.LineSpacing + 5;
            }

            position = new Vector2((Game.Window.ClientBounds.Width - width) / 2,
                                    (Game.Window.ClientBounds.Height - height) / 2);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (InputManager.IsActionPressed(InputManager.Action.CursorDown))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            else if (InputManager.IsActionPressed(InputManager.Action.CursorUp))
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
            Vector2 location = position;
            Color tint;

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                    tint = highlight;
                else
                    tint = normal;
                spriteBatch.DrawString(spriteFont, menuItems[i], location, tint);
                location.Y += spriteFont.LineSpacing + 5;
            }
        }
    }
}
