using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaActionLibrary.SpriteClasses;
using XnaActionLibrary.TileEngine;

namespace WindowsGame1
{
    /// <summary>
    /// Defines the different enemy types for the game.
    /// </summary>
    public enum EnemyType
    {
        Turret,
    }

    /// <summary>
    /// This class manages all sprites in the game.  It is responsible for their creation, as well as updating and drawing them.
    /// </summary>
    public sealed class SpriteManager
    {
        #region Fields

        public List<AnimatedSprite> SpriteList = new List<AnimatedSprite>();

        #endregion

        #region Constructors

        private SpriteManager()
        {         
        }

        static readonly SpriteManager spriteManagerInstance = new SpriteManager();
        public static SpriteManager Instance
        {
            get { return spriteManagerInstance; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new player for the level.  It's position is updated and then added to the list of animated sprites in the game.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Player CreatePlayer(Vector2 position)
        {
            Player player = null;
            player.Position = position;
            Instance.SpriteList.Add(player);
            return player;
        }

        /// <summary>
        /// Creates a new enemy for the level based on its type.  It's position is updated and then added to the list of animated sprites in the game.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public Enemy CreateEnemy(EnemyType type, Vector2 position)
        {
            Enemy newEnemy = null;
            switch (type)
            {
                case EnemyType.Turret:
                    newEnemy = new Turret();
                    break;
            }

            newEnemy.Position = position;
            Instance.SpriteList.Add(newEnemy);
            return newEnemy;
        }

        /// <summary>
        /// Update all sprites in the game.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (AnimatedSprite sprite in SpriteList)
            {
                sprite.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw all sprites in the game.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="camera"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            foreach (AnimatedSprite sprite in SpriteList)
            {
                sprite.Draw(gameTime, spriteBatch, camera);
            }
        }

        #endregion

    }
}
