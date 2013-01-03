using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using XnaActionLibrary.TileEngine;
using WindowsGame1.Sprites;

namespace WindowsGame1.Levels
{
    /// <summary>
    /// The first level in the game.
    /// </summary>
    public class LevelOne : Level
    {
        Tileset tileset1;
        private int numEvents = 5;

        // Constructs a level.  
        public LevelOne(IServiceProvider serviceProvider, Game1 game, SpriteBatch spriteBatch, int numLayers) 
            : base(serviceProvider, game, spriteBatch, numLayers)
        {
            eventList = new List<bool>();
            for (int i = 0; i < numEvents; i++)
                eventList.Add(false);
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

            string[] sampleConvo = { "Text 1", "Text 2", "Finished." };
            game.dialogueScreen.ConversationItems = sampleConvo;

            tilesets.Add(tileset1);

            LevelIndex = 1;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);               
        }

    }
}
