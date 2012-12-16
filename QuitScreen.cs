using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaActionLibrary;

namespace WindowsGame1
{
    class QuitScreen : GameScreen
    {
        MenuComponent menuComponent;
        Texture2D image;
        Rectangle imageRectangle;

        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }

        public QuitScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D image)
            : base(game, spriteBatch)
        {
            string[] menuItems = { "Yes", "No" };
            menuComponent = new MenuComponent(game, spriteBatch, spriteFont, menuItems);
            Components.Add(menuComponent);
            this.image = image;
            imageRectangle = new Rectangle((Game.Window.ClientBounds.Width - this.image.Width) / 2, (Game.Window.ClientBounds.Height - this.image.Height) / 2,
                                            this.image.Width, this.image.Height);
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
