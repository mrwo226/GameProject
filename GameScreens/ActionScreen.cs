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
    /// This is a class that inherits from the base class GameScreen to provide the main gameplay screen for the user.  The action screen manages the levels for
    /// the game.
    /// </summary>
    class ActionScreen : GameScreen
    {
        #region Fields

        LevelOne level; // The level the player is currently on.
        int levelIndex; // The numerical representation of the current level.
        Player player;
        Vector2 playerSpawn;
        Game1 game;

        #endregion

        #region Constructor

        public ActionScreen(Game1 game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            // For now, set the level index as if it were a new game.
            levelIndex = 0;
            this.game = game;
            LoadNextLevel();
            player = new Player(game, playerSpawn, level.Camera);
            SpriteManager.Instance.SpriteList.Add(player);
            level.Camera.LockToSprite(player);
        }

        #endregion

        #region Methods

        private void LoadNextLevel()
        {
            // Move to the next level
            levelIndex = levelIndex + 1;

            // Unloads the content for the current level before loading the next one.
            if (level != null)
                level.Dispose();

            level = new LevelOne(game.Services, game, spriteBatch, 1);
            playerSpawn= level.playerStart;        
        }

        private void ReloadCurrentLevel()
        {
            --levelIndex;
            LoadNextLevel();
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            level.Update(gameTime);
            player.Update(gameTime);
            base.Update(gameTime);
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            level.Draw(spriteBatch);
            player.Draw(gameTime, spriteBatch, level.Camera);
            base.Draw(gameTime);
        }

        #endregion
    }
}
