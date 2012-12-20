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
    public class LevelOne : Level
    {
        Tileset tileset1; 
        Tileset tileset2;

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

            tilesetTexture = Content.Load<Texture2D>("Tilesets/Level1Tileset1");
            tileset1 = new Tileset(tilesetTexture, 8, 8, 64, 64);

            tilesets.Add(tileset1);

            LevelIndex = 1;
            playerStart = new Vector2(game.screenRectangle.Width / 2, game.screenRectangle.Height / 2);

        }

    }
}
