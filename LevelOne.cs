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
        Tileset tileset1; //
        Tileset tileset2;

        // Constructs a level.  
        public LevelOne(IServiceProvider serviceProvider, Game1 game, Stream fileStream, SpriteBatch spriteBatch, int numLayers) 
            : base(serviceProvider, game, fileStream, spriteBatch, numLayers)
        {
        }

        /// <summary>
        /// Loads the images used for this level.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            tilesetTexture = Content.Load<Texture2D>("Tilesets/tileset1");
            tileset1 = new Tileset(tilesetTexture, 8, 8, 32, 32);

            tilesetTexture = Content.Load<Texture2D>("Tilesets/tileset2");
            tileset2 = new Tileset(tilesetTexture, 8, 8, 32, 32);

            tilesets.Add(tileset1);
            tilesets.Add(tileset2);

        }

        // Unloads the level content
        public void Dispose()
        {
            Content.Unload();
        }
    }
}
