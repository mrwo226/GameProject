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
using XnaActionLibrary.Collisions;
using XnaActionLibrary.SpriteClasses;

namespace WindowsGame1
{
    /// <summary>
    /// This level defines fields, properties, and methods that all levels in the game will share.  The Level class is responsible for using the implementation
    /// of the tile engine to draw the level to the screen.  It references text files created in an external file editor, Tiled, to gain information about which tiles
    /// should be drawn to the screen and where.  In addition, it also references two other provided text files (also created in Tiled), "collision.txt" and 
    /// "sprite.txt".  'collision.txt' contains information about where collisions should be located in the level, and which type they are.  These properties are
    /// read and then set to the corresponding tile in the layer.  'sprite.txt' contains information about sprite spawn positions.  It reads the information
    /// and references the sprite manager to create the indicated sprites and place then on the the level in the appropriate positions.
    /// </summary>
    public abstract class Level
    {
        #region Fields

        Engine engine = new Engine();  // The engine used to hold tile information for this level.
        TileMap map; // The tilemap containing the tilesets and tile layers for this level.
        public Texture2D tilesetTexture; // The tileset containing the images used for tiles.
        public Texture2D layerTexture; // Texture that holds the images for the background layers.
        public List<Tileset> tilesets; // The tilesets for this level.
        int levelWidth; // The width of the level in tiles.
        int levelHeight; // The height of the level in tiles.
        SpriteBatch spriteBatch; // Used to draw the level to the screen.
        Camera camera; // The scrolling camera.
        ContentManager content; // A new content manager to load and unload content for the current level.
        int levelIndex; // The number of this level.  
        public Vector2 playerStart; // The starting position for the player.
        public Game1 game; // A reference to this game object, used in constructing the content manager.

        #endregion

        #region Properties

        public ContentManager Content
        {
            get { return content; }
        }

        public int LevelIndex
        {
            get { return levelIndex; }
            set { levelIndex = value; }
        }

        public Camera Camera
        {
            get { return camera; }
        }

        #endregion

        #region Constructor

        // Constructs a level.  
        public Level(IServiceProvider serviceProvider, Game1 game, SpriteBatch spriteBatch, int numLayers)
        {
            this.spriteBatch = spriteBatch;
            this.game = game;
            // Create a new content manager to load content used just by this level.
            content = new ContentManager(serviceProvider, "Content");
            LoadContent();
            LoadTiles(numLayers);
            //SpawnSprites();
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

        /// <summary>
        /// A function used for reading all files for this level.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public virtual List<string> readFile(Stream fileStream)
        {

            // Load each level from the text file line by line and ensure all the lines are the same length.  The width of the level in tiles is determined
            // by the number of elements, which is also the number of commas.  Each line is added to a list of lines and returned.
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                levelWidth = 0;
                foreach (char c in line)
                    if (c == ',') levelWidth++;
                while (line != null)
                {
                    lines.Add(line);
                    int length = 0;
                    foreach (char c in line)
                        if (c == ',') length++;
                    if (length != levelWidth)
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
        public virtual void LoadTiles(int numLayers)
        {
            List<MapLayer> mapLayers = new List<MapLayer>();
            List<string> lines;

            // Loop through every single layer.
            for (int i = 0; i < numLayers; i++)
            {
                // Load the text file that has the assigned tile positions.
                string levelPath = string.Format("Content/Levels/{0}/{1}.txt", levelIndex, i);
                using (Stream fileStream = TitleContainer.OpenStream(levelPath))
                {
                    lines = new List<string>();
                    lines = readFile(fileStream);
                }

                // The level is as wide as the number of elements in the line, or the number of commas, and as high as the number of lines.
                levelHeight = lines.Count;

                // Create a new layer.
                MapLayer layer = new MapLayer(levelWidth, levelHeight);

                    // Loop over every tile position in the file and set each tile to the layer.
                    for (int y = 0; y < layer.Height; y++)
                    {
                        string[] tileNumbers = lines[y].Split(',');
                        for (int x = 0; x < layer.Width; x++)
                        {
                            // Use the numbers from the file by converting them to integers.
                            int tileIndex = Convert.ToInt32(tileNumbers[x]);
                            Tile tile = new Tile(tileIndex - 1, i);
                            layer.SetTile(x, y, tile);
                            // Set the tile collisions.
                            //if (i == 0)
                                //LoadCollisions(layer);   
                        }
                    }
                    mapLayers.Add(layer);
            }

            // Create the tilemap with the tilesets and the layers.
            map = new TileMap(tilesets, mapLayers);
        }

        /// <summary>
        /// Sets each tile collision by using the information in a file provided by an external tile editor.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="levelIndex"></param>
        public virtual void LoadCollisions(MapLayer layer)
        {
            List<string> lines;
            CollisionType collisionType;

            // Load the text file that has the assigned collisions.
            string levelPath = string.Format("Content/Levels/{0}/collision.txt", levelIndex);
            using (Stream fileStream = TitleContainer.OpenStream(levelPath))
            {
                lines = new List<string>();
                lines = readFile(fileStream);
            }

                // Loop over every collision number in the file and set each collision to the corresponding tile.
                for (int y = 0; y < layer.Height; y++)
                {
                    string[] tileNumbers = lines[y].Split(',');
                    for (int x = 0; x < layer.Width; x++)
                    {
                        // Use the numbers from the file by converting them to integers.
                        int collisionNum = Convert.ToInt32(tileNumbers[x]);
                        if (collisionNum == 1)
                            collisionType = CollisionType.Impassable;
                        else
                            collisionType = CollisionType.Passable;
                        layer.SetTileCollision(x, y, collisionType);       
                    }
                }
            }

        /// <summary>
        /// Sets each sprite spawn position and sprite type by using the information in a file provided by an external tile editor.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="levelIndex"></param>
        public virtual void SpawnSprites()
        {
            List<string> lines;

            // Load the text file that has the assigned spawn positions.
            string levelPath = string.Format("Content/Levels/{0}/sprite.txt", levelIndex);
            using (Stream fileStream = TitleContainer.OpenStream(levelPath))
            {
                lines = new List<string>();
                lines = readFile(fileStream);
                fileStream.Close();
            }

            // Loop over every spawn number in the file and set each collision to the corresponding tile.
            for (int y = 0; y < levelHeight; y++)
            {
                string[] spriteNumbers = lines[y].Split(',');
                for (int x = 0; x < levelWidth; x++)
                {
                    // Use the numbers from the file by converting them to integers.
                    int spriteNum = Convert.ToInt32(spriteNumbers[x]);
                    Vector2 spritePosition = new Vector2(x * Engine.TileWidth, y * Engine.TileHeight);
                    // The player is instantiated at the beginning of the game, so it's starting position is merely updated for this level.
                    // The enemies for this level, however, are created here using the SpriteManager.
                    if (spriteNum == 1)
                        playerStart = spritePosition;
                    else if (spriteNum == 2)
                        SpriteManager.Instance.CreateEnemy(EnemyType.Turret, spritePosition);
                }
            }
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
            spriteBatch.Draw(layerTexture, Vector2.Zero, Color.White);
            map.Draw(spriteBatch, camera);
        }

        #endregion
    }
}
