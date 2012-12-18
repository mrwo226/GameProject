using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using XnaActionLibrary;
using XnaActionLibrary.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    public abstract class Level
    {
        #region Fields

        Engine engine = new Engine();  // The engine used to hold tile information for this level.
        TileMap map; // The tilemap containing.
        public Texture2D tilesetTexture; // The tileset containing the images used for tiles.
        public List<Tileset> tilesets;
        SpriteBatch spriteBatch;
        Camera camera;
        ContentManager content; // A new content manager to load and unload content for the current level.

        #endregion

        #region Properties

        public ContentManager Content
        {
            get { return content; }
        }

        #endregion

        #region Constructor

        // Constructs a level.  
        public Level(IServiceProvider serviceProvider, Game1 game, Stream fileStream, SpriteBatch spriteBatch, int numLayers)
        {
            this.spriteBatch = spriteBatch;
            // Create a new content manager to load content used just by this level.
            content = new ContentManager(serviceProvider, "Content");
            LoadContent();
            LoadTiles(fileStream, numLayers);
            camera = new Camera(game.screenRectangle);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the images used for this level.
        /// </summary>
        public virtual void LoadContent()
        {
            tilesets = new List<Tileset>();
        }

        public virtual List<string> readFile(Stream fileStream)
        {
            // Load each level from the text file line by line and ensure all the lines are the same length.
            int width;
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                        throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));
                    line = reader.ReadLine();
                }
            }

            return lines;
        }

        /// <summary>
        /// Loads the tiles, getting their position and type from a provided file created in an external tile editor. 
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="levelIndex"></param>
        public virtual void LoadTiles(Stream fileStream, int numLayers)
        {

           // Load the level and ensure all the lines are the same length.
            List<string> lines = readFile(fileStream);

            // The level is as wide as the number of characters in a line minus the commas, and as high as the number of lines.
            int levelHeight = lines.Count;
            int width = lines[0].Length / 2;
            List<MapLayer> mapLayers = new List<MapLayer>();

            // Loop through every single layer.
            for (int i = 0; i < numLayers; i++)
            {
                // Create a new layer to assign tiles to.
                MapLayer layer = new MapLayer(width, levelHeight);

                // Loop over every tile position in the file and set each tile to the layer.
                for (int y = 0; y < layer.Height; y++)
                {
                    string[] tileNumbers = lines[y].Split(',');
                    for (int x = 0; x < layer.Width; x++)
                    {
                        // Use the numbers from the file by converting them to integers.
                        int tileIndex = Convert.ToInt32(tileNumbers[x]);
                        Tile tile = new Tile(tileIndex - 1, 0);
                        layer.SetTile(x, y, tile);
                    }
                }
                mapLayers.Add(layer);
            }

            // Create the tilemap with the tilesets and the layers.
            map = new TileMap(tilesets, mapLayers);
        }

        // Unloads the level content
        public void Dispose()
        {
            Content.Unload();
        }

        #endregion

        #region Update

        // Updates the camera scrolling.
        public void Update(GameTime gameTime)
        {
            camera.Update(gameTime);
        }

        #endregion

        #region Draw

        // Draws the map to the screen.
        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch, camera);
        }

        #endregion
    }
}
