using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XnaActionLibrary;
using XnaActionLibrary.SpriteClasses;
using XnaActionLibrary.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1.Levels;
using WindowsGame1.Sprites;

namespace WindowsGame1.Projectiles
{ 
    /// <summary>
    /// Defines a new Projectile, an object that does damage to sprites and/or the environment in the game.
    /// </summary>
    public abstract class Projectile
    {
        #region Fields

        public Texture2D projectileTexture; // The texture that holds the projectile graphic.
        Vector2 position; // The projectile's position in the game screen.
        Vector2 velocity; // The projectile's velocity.
        Vector2 motion; // The vector representing the direction of the projectile's motion.
        CardinalDirection orientation; // A cardinal direction representation of the projectile's motion.
        float rotation; // The angle that the projectile is rotated.
        float speed; // The speed of the projectile.
        bool isActive; // The boolean that determines if the projectile is active or not.
        int damage; // The amount of damage the projectile does.
        ProjectileAlignment alignment; // Does the projectile belong to friend or foe?

        #endregion

        #region Properties

        public virtual int Width
        {
            get { return projectileTexture.Width; }
        }

        public virtual int Height
        {
            get { return projectileTexture.Height; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(value, 0.0f, 20.0f); }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Motion
        {
            get { return motion; }
            set { motion = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set
            {
                velocity = value;
                if (velocity != Vector2.Zero)
                    velocity.Normalize();
            }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public CardinalDirection Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        public int Damage
        {
            get { return damage; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public ProjectileAlignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }

        // Gets a texture origin at the center of each frame
        public virtual Vector2 Origin
        {
            get { return new Vector2(Width / 2.0f, Height / 2.0f); }
        }

        // A bounding rectangle used for checking collisions.
        public Rectangle BoundingRectangle
        {
            get { return new Rectangle((int)position.X - Width / 2, (int)position.Y - Height / 2, projectileTexture.Width, projectileTexture.Height); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new Projectile object.  All projectiles will begin with their orientation/rotation set at the sprite's orientation/rotation.
        /// </summary>
        public Projectile()
        {
            orientation = SpriteManager.Instance.SpriteList[0].Orientation;
            rotation = SpriteManager.Instance.SpriteList[0].Rotation;
            IsActive = true;
        }

        #endregion

        /// <summary>
        /// Determines the current rotation of the sprite based on it's cardinal direction.  The rotation angle is specified in radians.
        /// </summary>
        public virtual void DetermineMotion()
        {
            Motion = Vector2.Zero;
            if (Orientation == CardinalDirection.North)
            {
                motion.X = 0;
                motion.Y = -1;
            }
            if (Orientation == CardinalDirection.Northwest)
            {
                motion.X = -1;
                motion.Y = -1;
            }
            if (Orientation == CardinalDirection.West)
            {
                motion.X = -1;
                motion.Y = 0;
            }
            if (Orientation == CardinalDirection.Southwest)
            {
                motion.X = -1;
                motion.Y = 1;
            }
            if (Orientation == CardinalDirection.South)
            {
                motion.X = 0;
                motion.Y = 1;
            }
            if (Orientation == CardinalDirection.Southeast)
            {
                motion.X = 1;
                motion.Y = 1;
            }
            if (Orientation == CardinalDirection.East)
            {
                motion.X = 1;
                motion.Y = 0;
            }
            if (Orientation == CardinalDirection.Northeast)
            {
                motion.X = 1;
                motion.Y = -1;
            }
        }

        /// <summary>
        /// All projectiles will update their motion, cardinal direction, and rotation.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            DetermineMotion();
            if (motion != Vector2.Zero)
            {
                Velocity = Motion * Speed;
                Position += Velocity * Speed;
            }

            // Deactive the laser if it goes off the screen.
            if (Position.X < 0 || Position.X > LevelManager.Instance.currentLevel.levelLayer.Width * Engine.TileWidth)
                isActive = false;
            if (Position.Y < 0 || Position.Y > LevelManager.Instance.currentLevel.levelLayer.Height * Engine.TileHeight)
                isActive = false;
        }

        /// <summary>
        /// Draws the projectile to the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(projectileTexture, Position - camera.Position, null, Color.White, rotation, Origin, .50f, SpriteEffects.None, 0);
        }
    }
}
