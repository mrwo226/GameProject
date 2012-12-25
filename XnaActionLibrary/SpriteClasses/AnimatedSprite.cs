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
        Direction orientation; // A cardinal direction representation of the sprite's motion.
        float rotation; // The angle that the sprite is rotated.
        float speed; // The speed of the sprite.

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

        public Direction Orientation
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

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new AnimatedSprite object.  All sprites will begin with their orientation at East.
        /// </summary>
        public AnimatedSprite()
        {
            Orientation = Direction.East;
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
        /// Determines the current direction the sprite is moving in cardinal directions.
        /// </summary>
        public virtual void determineCurrentDirection()
        {
            if (motion.X > 0)
            {
                if (motion.Y > 0)
                    orientation = Direction.Southeast;
                else if (motion.Y < 0)
                    orientation = Direction.Northeast;
                else // y == 0
                    orientation = Direction.East;
            }
            else if (motion.X < 0)
            {
                if (motion.Y > 0)
                    orientation = Direction.Southwest;
                else if (motion.Y < 0)
                    orientation = Direction.Northwest;
                else // y == 0
                    orientation = Direction.West;
            }
            else // x == 0
            {
                if (motion.Y > 0)
                    orientation = Direction.South;
                else if (motion.Y < 0)
                    orientation = Direction.North;
            }
        }

        /// <summary>
        /// Determines the current rotation of the sprite based on it's cardinal direction.  The rotation angle is specified in radians.
        /// </summary>
        public virtual void determineRotation()
        {
            if (Orientation == Direction.North)
                rotation = -MathHelper.Pi / 2.0f;
            if (Orientation == Direction.Northwest)
                rotation = -3 * MathHelper.Pi / 4.0f;
            if (Orientation == Direction.West)
                rotation = -MathHelper.Pi;
            if (Orientation == Direction.Southwest)
                rotation = -5 * MathHelper.Pi / 4.0f;
            if (Orientation == Direction.South)
                rotation = -3 * MathHelper.Pi / 2.0f;
            if (Orientation == Direction.Southeast)
                rotation = -7 * MathHelper.Pi / 4.0f;
            if (Orientation == Direction.East)
                rotation = 0f;
            if (Orientation == Direction.Northeast)
                rotation = -MathHelper.Pi / 4.0f;
        }

        /// <summary>
        /// All sprites will update their motion, cardinal direction, rotation, and animation.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            determineCurrentDirection();
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
