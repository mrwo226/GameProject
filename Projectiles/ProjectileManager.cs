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
using WindowsGame1.Sprites;

namespace WindowsGame1.Projectiles
{
    /// <summary>
    /// Defines the different projectile types for the game.
    /// </summary>
    public enum ProjectileType
    {
        BasicLaser,
        ChargedLaser,
        Bullet,
        Rocket,
    }

    /// <summary>
    /// Defines the ownership of a projectile for determining collisions and damage.
    /// </summary>
    public enum ProjectileAlignment
    {
        Friendly,
        Enemy,
        Neutral,
    }

    /// <summary>
    /// This class manages all projectiles in the game.  It is responsible for their creation and collisions, as well as updating and drawing them.
    /// </summary>
    public sealed class ProjectileManager
    {
        #region Fields

        public List<Projectile> ProjectileList = new List<Projectile>();

        #endregion

        #region Constructors

        private ProjectileManager()
        {
        }

        static readonly ProjectileManager projectileManagerInstance = new ProjectileManager();
        public static ProjectileManager Instance
        {
            get { return projectileManagerInstance; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new enemy for the level based on its type.  It's position is updated and then added to the list of animated sprites in the game.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public void CreateProjectile(ProjectileType type, Vector2 spawnPosition, Game game, ProjectileAlignment alignment)
        {
            Projectile newProjectile = null;
            switch (type)
            {
                case ProjectileType.BasicLaser:
                    newProjectile = new BasicLaser(game, spawnPosition, alignment);
                    break;
            }

            ProjectileList.Add(newProjectile);
            
        }

        public void HandleProjectileToTileCollisions()
        {
            for (int i = 0; i < ProjectileList.Count; i++)
            {
                // Get the player's bounding rectangle and find any neighboring tiles
                Rectangle bounds = ProjectileList[i].BoundingRectangle;
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
                                if (collisionType == CollisionType.Impassable)
                                {
                                    // Resolve the collision along the X axis.
                                    ProjectileList[i].IsActive = false;
                                }
                            }
                        }
                    }
            }
        }

        /// <summary>
        /// Deletes all projectiles when the level is unloaded.
        /// </summary>
        /// <param name="gameTime"></param>
        public void DeleteProjectiles()
        {
            for (int i = ProjectileList.Count - 1; i >= 0; i--)
            {
                ProjectileList.RemoveAt(i);
            }
        }

        /// <summary>
        /// Update all sprites in the game.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < ProjectileList.Count; i++)
            {
                ProjectileList[i].Update(gameTime);
                HandleProjectileToTileCollisions();
                if (ProjectileList[i].IsActive == false)
                    ProjectileList.RemoveAt(i);
            }
        }

        /// <summary>
        /// Draw all sprites in the game.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            foreach (Projectile projectile in ProjectileList)
            {
                projectile.Draw(gameTime, spriteBatch, camera);
            }
        }

        #endregion

    }
}
