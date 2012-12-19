using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaActionLibrary;
using XnaActionLibrary.TileEngine;

namespace WindowsGame1
{
    public class Player
    {
        #region Fields

        Camera camera;
        Game game;

        Texture2D playerTexture;
        Animation normalAnimation;
        Animation currentAnimation;

        Vector2 position;

        #endregion

        #region Properties 

        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        #endregion

        #region Constructor

        public Player(Game1 game, Vector2 position)
        {
            this.game = (Game1)game;
            Position = position;
            LoadContent();
        }

        public void LoadContent()
        {
            playerTexture = game.Content.Load<Texture2D>("Sprites/Player/normalAnimation");
            normalAnimation = new Animation(playerTexture, position, 64, 64, 4, 100, Color.White, 1f, true);
            currentAnimation = normalAnimation;
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            normalAnimation.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            normalAnimation.Draw(spriteBatch);
        }

        #endregion

    }
}
