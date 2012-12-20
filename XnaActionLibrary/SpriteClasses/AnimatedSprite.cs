using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaActionLibrary.TileEngine;

namespace XnaActionLibrary.SpriteClasses
{
    public class AnimatedSprite
    {
        #region Fields

        Animation currentAnimation;
        public Texture2D spriteTexture;
        Vector2 position;
        Vector2 velocity;
        Vector2 motion;
        Direction orientation;
        float rotation;
        float speed = 2.0f;

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
            set { speed = value; }
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

        public AnimatedSprite()
        {
            Orientation = Direction.East;
        }

        #endregion

        public virtual void LockToMap()
        {
            position.X = MathHelper.Clamp(position.X, 0 + Width / 2, TileMap.WidthInPixels - Width / 2);
            position.Y = MathHelper.Clamp(position.Y, 0 + Width / 2, TileMap.HeightInPixels - Height / 2 );
        }

        public virtual void SetAnimation(Animation newAnimation)
        {
            currentAnimation.isActive = false;
            currentAnimation = newAnimation;
            currentAnimation.isActive = true;
        }

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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(spriteTexture, position - camera.Position, currentAnimation.SourceRectangle, Color.White, rotation, Origin, 1, SpriteEffects.None, 0);
        }

    }
}
