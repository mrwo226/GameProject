using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaActionLibrary.TileEngine
{
    public class Camera
    {
        #region Fields

        Vector2 position;
        float speed;
        float zoom;
        Rectangle viewportRectangle;

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

        #endregion

        #region Constructors

        public Camera(Rectangle viewportRectangle)
        {
            speed = 4f;
            zoom = 1f;
            this.viewportRectangle = viewportRectangle;
        }

        public Camera(Rectangle viewportRectangle, Vector2 position)
        {
            speed = 4f;
            zoom = 1f;
            Position = position;
            this.viewportRectangle = viewportRectangle;
        }

        #endregion

        #region Methods

        public void LockCamera()
        {
            position.X = MathHelper.Clamp(position.X, 0, TileMap.WidthInPixels - viewportRectangle.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, TileMap.HeightInPixels - viewportRectangle.Height);
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
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
                motion.Normalize();

            position += motion * speed;

            LockCamera();
        }

        #endregion
    }
}
