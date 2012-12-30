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
    /// This is a class that inherits from the base class GameScreen to provide a dialogue window for the user.  The user can cycle through the text that is
    /// drawn to the window.  When all the text has been shown, the window goes away.
    /// </summary>
    public class DialogueScreen : GameScreen
    {
        #region Fields

        Texture2D image; // Holds the image used for the dialogue window.
        Rectangle imageRectangle; // Holds the size and location of the image.
        ActionScreen actionScreen; // The screen the conversation window appears on top of.
        SpriteFont spriteFont; // The Sprite Font used to draw the text to the screen.
        string[] conversationItems; // The dialogue set to appear for this particular conversation.
        Vector2 location; // The location of the text in the window.
        int index; // The current text displayed to the window.
        bool isComplete; // Keeps track of whether or not all of the text has appeared.

        #endregion

        #region Properties

        // When new conversation items are added to the screen, it becomes active.
        public string[] ConversationItems
        {
            get { return conversationItems; }
            set 
            {   
                conversationItems = value;
                isComplete = false;
                index = 0;
            }
        }

        public bool IsComplete
        {
            get { return isComplete; }
        }

        #endregion

        #region Constructor

        public DialogueScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D image, ActionScreen actionScreen)
            : base(game, spriteBatch)
        {
            // Set the image and calculate its size.
            this.image = image;
            imageRectangle = new Rectangle((Game.Window.ClientBounds.Width - this.image.Width) / 2, (Game.Window.ClientBounds.Height - this.image.Height),
                                            this.image.Width, this.image.Height);
            location = new Vector2(100, (Game.Window.ClientBounds.Height - this.image.Height / 2 - 20));
            this.spriteFont = spriteFont;
            this.actionScreen = actionScreen;
            isComplete = true;
            index = 0;
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            // If the user hits 'okay', show the next section of text.
            if (InputManager.IsActionTriggered(InputManager.Action.Ok))
                index++;

            // If all of the text has been shown, then set the boolean to true.
            if (index == conversationItems.Length)
                isComplete = true;
            
            base.Update(gameTime);
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            
            // Render the action screen underneath the dialogue window.
            actionScreen.Draw(gameTime);

            // Only render the image if the user hasn't cycled through all of the text.
            if (isComplete == false)
            {
                spriteBatch.Draw(image, imageRectangle, Color.White);
                spriteBatch.DrawString(spriteFont, conversationItems[index], location, Color.White);
            }
            
        }

        #endregion
    }
}
