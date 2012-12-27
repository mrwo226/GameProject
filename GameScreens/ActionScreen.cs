using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using WindowsGame1.GameScreens;
using XnaActionLibrary.SpriteClasses;
using XnaActionLibrary;
using XnaActionLibrary.TileEngine;

namespace WindowsGame1.GameScreens
{
    /// <summary>
    /// This is a class that inherits from the base class GameScreen to provide the main gameplay screen for the user.  The action screen updates and draws
    /// the levels and the sprites.
    /// </summary>
    class ActionScreen : GameScreen
    {
        #region Fields


        #endregion

        #region Constructor

        public ActionScreen(Game1 game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            this.game = game;
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            LevelManager.Instance.Update(gameTime);
            SpriteManager.Instance.Update(gameTime);
            ProjectileManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            LevelManager.Instance.currentLevel.Draw(spriteBatch);
            SpriteManager.Instance.Draw(gameTime, spriteBatch, LevelManager.Instance.currentLevel.Camera);
            ProjectileManager.Instance.Draw(gameTime, spriteBatch, LevelManager.Instance.currentLevel.Camera);
            base.Draw(gameTime);
        }

        #endregion
    }
}
