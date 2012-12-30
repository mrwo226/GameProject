using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaActionLibrary.SpriteClasses;
using XnaActionLibrary.TileEngine;
using XnaActionLibrary.Collisions;
using WindowsGame1.Levels;

namespace WindowsGame1.Sprites
{
    /// <summary>
    /// Defines the different enemy types for the game.
    /// </summary>
    public enum EnemyType
    {
        Turret,
    }

    /// <summary>
    /// This class manages all sprites in the game.  It is responsible for their creation and collisions, as well as updating and drawing them.
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
        public Player CreatePlayer(Vector2 position, Game1 game, Camera camera)
        {
            Player player = new Player(game, position, camera);
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

        public void HandleSpriteToTileCollisions()
        {
            foreach (AnimatedSprite sprite in SpriteList)
            {
                // Get the player's bounding rectangle and find any neighboring tiles
                Rectangle bounds = sprite.BoundingRectangle;
                int leftTile = (int)Math.Floor((float)bounds.Left / Engine.TileWidth);
                int rightTile = (int)Math.Ceiling((float)bounds.Right / Engine.TileWidth);
                int topTile = (int)Math.Floor((float)bounds.Top / Engine.TileHeight);
                int bottomTile = (int)Math.Ceiling((float)bounds.Bottom / Engine.TileHeight);

                // For each potentially colliding tile...
                for (int y = topTile; y <= bottomTile; ++y)
                    for (int x = leftTile; x <= rightTile; ++x)
                    {
                        MapLayer tileLayer = LevelManager.Instance.currentLevel.levelLayer;
                        CollisionType collisionType = tileLayer.GetTile(x, y).TileCollision;

                        if (collisionType != CollisionType.Passable)
                        {
                            // Determine the collision depth (with direction) and magnitude.
                            Rectangle tileBounds = tileLayer.GetTile(x, y).BoundingRectangle;
                            Vector2 depth = RectangleExtensions.GetIntersectionDepth(bounds, tileBounds);

                            if (depth != Vector2.Zero)
                            {
                                float absDepthX = Math.Abs(depth.X);
                                float absDepthY = Math.Abs(depth.Y);

                                // Resolve the collision along the shallow axis.
                                if (absDepthY < absDepthX)
                                {
                                    if (collisionType == CollisionType.Impassable)
                                    {
                                        // Resolve the collision along the Y axis.
                                        sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y + depth.Y);

                                        // Perform further collisions with the new bounds.
                                        bounds = sprite.BoundingRectangle;
                                    }
                                }
                                else if (collisionType == CollisionType.Impassable)
                                {
                                    // Resolve the collision along the X axis.
                                    sprite.Position = new Vector2(sprite.Position.X + depth.X, sprite.Position.Y);

                                    // Perform further collisions with the new bounds.
                                    bounds = sprite.BoundingRectangle;
                                }
                            }
                        }
                    }
            }
        }

        /// <summary>
        /// Deletes all sprites when the level is unloaded.
        /// </summary>
        /// <param name="gameTime"></param>
        public void DeleteSprites()
        {
            for (int i = SpriteList.Count - 1; i >= 0; i--)
            {
                SpriteList.RemoveAt(i);
            }
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
                HandleSpriteToTileCollisions();
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
