using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    /// <summary>
    /// This class manages the levels in the game.  It is responsible for loading them and reloading them.
    /// </summary>
    public sealed class LevelManager
    {
        #region Fields

        public Level currentLevel;
        public int levelNumber;
        public Vector2 playerSpawnPosition;

        #endregion

        #region Constructors

        private LevelManager()
        {
        }

        static readonly LevelManager levelManagerInstance = new LevelManager();
        public static LevelManager Instance
        {
            get { return levelManagerInstance; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the next level in the game.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        public void LoadNextLevel(Game1 game, SpriteBatch spriteBatch)
        {
            // Move to the next level
             levelNumber = levelNumber + 1;

            // Unloads the content for the current level before loading the next one.
             if (currentLevel != null)
             {
                 currentLevel.Dispose();
                 SpriteManager.Instance.DeleteSprites();
             }

            if (levelNumber == 1)
            {
                currentLevel = new LevelOne(game.Services, game, spriteBatch, 1);
                playerSpawnPosition = currentLevel.playerStart;
            }
        }

        /// <summary>
        /// Reloads the current level (in case of "game over", etc).
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        public void ReloadCurrentLevel(Game1 game, SpriteBatch spriteBatch)
        {
            --levelNumber;
            LoadNextLevel(game, spriteBatch);
        }

        /// <summary>
        /// Update the current level in the game.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime);
        }

        /// <summary>
        /// Draw the current level in the game.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="camera"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            currentLevel.Draw(spriteBatch);
        }

        #endregion

    }
}
