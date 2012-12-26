using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaActionLibrary.TileEngine;

namespace XnaActionLibrary.SpriteClasses
{
    /// <summary>
    /// This class specifies parameters that all animated sprites in the game will share.  Every sprite of this type will have the ability to move, turn, 
    /// and animate.
    /// </summary>
    public abstract class AnimatedSprite
    {
        #region Fields

        Animation currentAnimation; // The current animation that is playing for the sprite.
        public Texture2D spriteTexture; // The texture that holds the animation strip.
        Vector2 position; // The sprite's position in the game screen.
        Vector2 velocity; // The sprite's velocity.
        Vector2 motion; // The vector representing the direction of the sprite's motion.
        CardinalDirection orientation; // A cardinal direction representation of the sprite's motion.
        float rotation; // The angle that the sprite is rotated.
        float speed; // The speed of the sprite.
        bool isAlive;

        #endregion

        #region Properties

        public Animation CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }

        public int Width
        {
            get { return currentAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return currentAnimation.FrameHeight; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(value, 0.0f, 16.0f); }
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
        public float MotionX
        {
            get { return motion.X; }
            set { motion.X = value; }
        }
        public float MotionY
        {
            get { return motion.Y; }
            set { motion.Y = value; }
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

        public CardinalDirection Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        public float Rotation
        {
            get { return rotation; }
        }

        // Gets a texture origin at the center of each frame
        public Vector2 Origin
        {
            get { return new Vector2(currentAnimation.FrameWidth / 2.0f, currentAnimation.FrameHeight / 2.0f); }
        }

        // A bounding rectangle used for checking collisions.
        public Rectangle BoundingRectangle
        {
            get { return new Rectangle((int)position.X - Width / 2, (int)position.Y - Height / 2, Engine.TileWidth, Engine.TileHeight); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new AnimatedSprite object.  All sprites will begin with their orientation at East.
        /// </summary>
        public AnimatedSprite()
        {
            Orientation = CardinalDirection.East;
        }

        #endregion

        /// <summary>
        /// Keeps the sprite from moving off of the screen.
        /// </summary>
        public virtual void LockToMap()
        {
            position.X = MathHelper.Clamp(position.X, 0 + Width / 2, TileMap.WidthInPixels - Width / 2);
            position.Y = MathHelper.Clamp(position.Y, 0 + Width / 2, TileMap.HeightInPixels - Height / 2 );
        }

        /// <summary>
        /// Changes the sprite's animation to a new strip.
        /// </summary>
        /// <param name="newAnimation"> The new animation the sprite should have.</param>
        public virtual void SetAnimation(Animation newAnimation)
        {
            currentAnimation.isActive = false;
            currentAnimation = newAnimation;
            currentAnimation.isActive = true;
        }

        /// <summary>
        /// Determines the current rotation of the sprite based on it's cardinal direction.  The rotation angle is specified in radians.
        /// </summary>
        public virtual void determineRotation()
        {
            if (Orientation == CardinalDirection.North)
                rotation = -MathHelper.Pi / 2.0f;
            if (Orientation == CardinalDirection.Northwest)
                rotation = -3 * MathHelper.Pi / 4.0f;
            if (Orientation == CardinalDirection.West)
                rotation = -MathHelper.Pi;
            if (Orientation == CardinalDirection.Southwest)
                rotation = -5 * MathHelper.Pi / 4.0f;
            if (Orientation == CardinalDirection.South)
                rotation = -3 * MathHelper.Pi / 2.0f;
            if (Orientation == CardinalDirection.Southeast)
                rotation = -7 * MathHelper.Pi / 4.0f;
            if (Orientation == CardinalDirection.East)
                rotation = 0f;
            if (Orientation == CardinalDirection.Northeast)
                rotation = -MathHelper.Pi / 4.0f;
        }

        /// <summary>
        /// All sprites will update their motion, cardinal direction, rotation, and animation.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            Orientation = Direction.determineCurrentDirection(motion, Orientation);
            determineRotation();
            CurrentAnimation.Update(gameTime);
        }

        /// <summary>
        /// Draws the animated sprite to the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(spriteTexture, position - camera.Position, currentAnimation.SourceRectangle, Color.White, rotation, Origin, 1, SpriteEffects.None, 0);
        }
    }
}
