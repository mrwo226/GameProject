using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaActionLibrary.TileEngine
{
    /// <summary>
    /// Holds information about the tiles used in the game.  The tiles are 64x64.
    /// </summary>
    public class Engine
    {
        #region Field Region

        static int tileWidth = 64;
        static int tileHeight = 64;

        #endregion

        #region Property Region

        public static int TileWidth
        {
            get { return tileWidth; }
        }
        public static int TileHeight
        {
            get { return tileHeight; }
        }
        
        #endregion

        #region Constructor

        public Engine()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a particular position on the game screen in tiles.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Point VectorToCell(Vector2 position)
        {
            return new Point((int)position.X / tileWidth, (int)position.Y / tileHeight);
        }

        #endregion
    }
}
