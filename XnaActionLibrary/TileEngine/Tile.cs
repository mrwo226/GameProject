using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XnaActionLibrary.TileEngine
{
    /// <summary>
    /// Holds information about a tile's relation to its source tileset.
    /// </summary>
    public class Tile
    {
        #region Fields

        int tileIndex; // The location of the tile in the tileset.
        int tileset; // The tileset the tile is from.

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

        #endregion

        #region Constructor

        public Tile(int tileIndex, int tileset)
        {
            TileIndex = tileIndex;
            Tileset = tileset;
        }

        #endregion
    }
}
