using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XnaActionLibrary.Collisions;

namespace XnaActionLibrary.TileEngine
{
    /// <summary>
    /// Holds information about a tile's relation to its source tileset and the game world.
    /// </summary>
    public class Tile
    {
        #region Fields

        int tileIndex; // The location of the tile in the tileset.
        int tileset; // The tileset the tile is from.
        CollisionType tileCollision; // The type of collision the tile has.
        Vector2 position; // The location of the tile in the game world.

        #endregion

        #region Property Region

        public int TileIndex
        {
            get { return tileIndex; }
            private set { tileIndex = value; }
        }

        public int Tileset
        {
            get { return tileset; }
            private set { tileset = value; }
        }

        public CollisionType TileCollision
        {
            get { return tileCollision; }
            set { tileCollision = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            private set { position = value; }
        }
        
        // A bounding rectangle used for checking collisions.
        public Rectangle BoundingRectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, Engine.TileWidth, Engine.TileHeight); }
        }

        #endregion

        #region Constructor

        public Tile(int tileIndex, int tileset, int x, int y)
        {
            TileIndex = tileIndex;
            Tileset = tileset;
            position.X = x * Engine.TileWidth;
            position.Y = y * Engine.TileHeight;
        }

        #endregion
    }
}
