using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaActionLibrary;
using XnaActionLibrary.SpriteClasses;

namespace XnaActionLibrary.TileEngine
{
    /// <summary>
    /// Specifies two camera modes:
    ///     Free: The camera can be controlled by the user.
    ///     Follow: The camera follows the sprite.
    /// </summary>
    public enum CameraMode { Free, Follow }

    public class Camera
    {
        #region Fields

        Vector2 position; // The position of the camera in the game screen.
        float speed; // The speed the camera moves at.
        float zoom; // The level of zoom of the camera (1 is normal).
        Rectangle viewportRectangle; // The rectangle specifying the dimensions of the game screen.
        CameraMode mode; // Which mode the camera is in.

        #endregion

        #region Properties

        public Vector2 Position
        {
            get { return position; }
            private set { position = value; }
        }

        public float Speed
        {
            get { return speed; }
            private set { speed = (float)MathHelper.Clamp(speed, 1f, 16f); }
        }

        public float Zoom
        {
            get { return zoom; }
        }

        public CameraMode Mode
        {
            get { return mode; }
        }

        #endregion

        #region Constructors

        public Camera(Rectangle viewportRectangle)
        {
            speed = 4f;
            zoom = 1f;
            this.viewportRectangle = viewportRectangle;
            mode = CameraMode.Follow;
        }

        public Camera(Rectangle viewportRectangle, Vector2 position)
        {
            speed = 4f;
            zoom = 1f;
            Position = position;
            this.viewportRectangle = viewportRectangle;
            mode = CameraMode.Follow;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Keeps the camera from going off of the edge of the screen.
        /// </summary>
        public void LockCamera()
        {
            position.X = MathHelper.Clamp(position.X, 0, TileMap.WidthInPixels - viewportRectangle.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, TileMap.HeightInPixels - viewportRectangle.Height);
        }

        /// <summary>
        /// Locks the camera to a sprite so it follows the sprite.
        /// </summary>
        /// <param name="sprite"></param>
        public void LockToSprite(AnimatedSprite sprite)
        {
            position.X = sprite.Position.X + sprite.Width / 2 - (viewportRectangle.Width / 2);
            position.Y = sprite.Position.Y + sprite.Height / 2 - (viewportRectangle.Height / 2);
            LockCamera();
        }

        /// <summary>
        /// Changes the camera mode from free to follow or visa versa.
        /// </summary>
        public void ToggleCameraMode()
        {
            if (mode == CameraMode.Follow)
                mode = CameraMode.Free;
            else if (mode == CameraMode.Free)
                mode = CameraMode.Follow;
        }

        #endregion

        #region Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (mode == CameraMode.Follow)
                return;

            Vector2 motion = Vector2.Zero;

            if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterLeft))
                motion.X = -speed;
            else if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterRight))
                motion.X = speed;

            if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterUp))
                motion.Y = -speed;
            else if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterDown))
                motion.Y = speed;

            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                position += motion * speed;
                LockCamera();
            }
        }

        #endregion
    }
}
