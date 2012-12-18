using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaActionLibrary;
using XnaActionLibrary.TileEngine;

namespace WindowsGame1
{
    public class Player
    {
        #region Fields

        Camera camera;
        Game game;

        #endregion

        #region Properties 

        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        #endregion

        #region Constructor

        public Player(Game1 game)
        {
            this.game = (Game1)game;
            camera = new Camera(game.screenRectangle);
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            camera.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        #endregion

    }
}
