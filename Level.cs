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
    class Level
    {
        Engine engine = new Engine();  // The engine used to hold tile information for this level.
        Tileset tileset1; // The tileset that handles base images for this level.
        Tileset tileset2; // A tileset to handle the second layer.
        TileMap map; // The tilemap containing 
        Texture2D tilesetTexture; // The tileset containing the images used for tiles.
        ContentManager content; // A new content manager to load and unload content for the current level.
        SpriteBatch spriteBatch;
        Camera camera;

        public ContentManager Content
        {
            get { return content; }
        }

        // Constructs a level.  
        public Level(IServiceProvider serviceProvider, Game1 game, Stream fileStream, int levelIndex, SpriteBatch spriteBatch)
        {
            // Create a new content manager to load content used just by this level.
            content = new ContentManager(serviceProvider, "Content");
            this.spriteBatch = spriteBatch;
            LoadContent();
            LoadTiles(fileStream);
            camera = new Camera(game.screenRectangle);
        }

        /// <summary>
        /// Loads the images used for this level.
        /// </summary>
        public void LoadContent()
        {
            tilesetTexture = Content.Load<Texture2D>("Tilesets/tileset1");
            tileset1 = new Tileset(tilesetTexture, 8, 8, 32, 32);

            tilesetTexture = Content.Load<Texture2D>("Tilesets/tileset2");
            tileset2 = new Tileset(tilesetTexture, 8, 8, 32, 32);
            
        }

        public List<string> readFile(Stream fileStream)
        {
            // Load the level and ensure all the lines are the same length.
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
        public void LoadTiles(Stream fileStream)
        {
            List<Tileset> tilesets = new List<Tileset>();
            tilesets.Add(tileset1);
            tilesets.Add(tileset2);

            // Load the level and ensure all the lines are the same length.
            List<string> lines = readFile(fileStream);

            // The level is as wide as the number of characters in a line minus the commas, and as high as the number of lines.
            int levelHeight = lines.Count;
            int width = lines[0].Length / 2;
            MapLayer layer = new MapLayer(width, levelHeight);

            // Loop over every tile position and load each tile
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

            List<MapLayer> mapLayers = new List<MapLayer>();
            mapLayers.Add(layer);
            map = new TileMap(tilesets, mapLayers);
        }

        // Unloads the level content
        public void Dispose()
        {
            Content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            camera.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch, camera);
        }
    }
}
