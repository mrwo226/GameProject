using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using XnaActionLibrary.TileEngine;

namespace WindowsGame1
{
    /// <summary>
    /// The first level in the game.
    /// </summary>
    public class LevelOne : Level
    {
        Tileset tileset1; 

        // Constructs a level.  
        public LevelOne(IServiceProvider serviceProvider, Game1 game, SpriteBatch spriteBatch, int numLayers) 
            : base(serviceProvider, game, spriteBatch, numLayers)
        {
        }

        /// <summary>
        /// Loads the images used for this level.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            tilesetTexture = Content.Load<Texture2D>("Tilesets/SpaceStationTileset1");
            tileset1 = new Tileset(tilesetTexture, 6, 3, 64, 64);

            layerTexture = Content.Load<Texture2D>("Backgrounds/1-1");

            tilesets.Add(tileset1);

            LevelIndex = 1;

        }

    }
}
