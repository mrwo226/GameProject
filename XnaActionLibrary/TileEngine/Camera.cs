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
    public enum CameraMode { Free, Follow }

    public class Camera
    {
        #region Fields

        Vector2 position;
        float speed;
        float zoom;
        Rectangle viewportRectangle;
        CameraMode mode;

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

        public void LockCamera()
        {
            position.X = MathHelper.Clamp(position.X, 0, TileMap.WidthInPixels - viewportRectangle.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, TileMap.HeightInPixels - viewportRectangle.Height);
        }

        public void LockToSprite(AnimatedSprite sprite)
        {
            position.X = sprite.Position.X + sprite.Width / 2 - (viewportRectangle.Width / 2);
            position.Y = sprite.Position.Y + sprite.Height / 2 - (viewportRectangle.Height / 2);
            LockCamera();
        }

        public void ToggleCameraMode()
        {
            if (mode == CameraMode.Follow)
                mode = CameraMode.Free;
            else if (mode == CameraMode.Free)
                mode = CameraMode.Follow;
        }

        #endregion

        #region Update

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
