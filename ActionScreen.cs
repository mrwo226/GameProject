using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnaActionLibrary;
using XnaActionLibrary.TileEngine;

namespace WindowsGame1
{
    class ActionScreen : GameScreen
    {
        Engine engine = new Engine();
        Tileset tileset;
        TileMap map;
        Texture2D tilesetTexture;

        public ActionScreen(Game game, SpriteBatch spriteBatch, Texture2D tilesetTexture)
            : base(game, spriteBatch)
        {
            this.tilesetTexture = tilesetTexture;
            LoadTiles();
        }

        public void LoadTiles()
        {
            tileset = new Tileset(tilesetTexture, 8, 8, 32, 32);

            MapLayer layer = new MapLayer(40, 40);
            for (int y = 0; y < layer.Height; y++)
            {
                for (int x = 0; x < layer.Width; x++)
                {
                    Tile tile = new Tile(0, 0);
                    layer.SetTile(x, y, tile);
                }
            }
            map = new TileMap(tileset, layer);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            map.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
