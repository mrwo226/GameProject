using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaActionLibrary;
using XnaActionLibrary.SpriteClasses;
using XnaActionLibrary.TileEngine;

namespace WindowsGame1
{
    public class Player : AnimatedSprite
    {
        #region Fields

        Game game;
        Animation normalAnimation;
        Camera camera;

        #endregion

        #region Properties 

        #endregion

        #region Constructor

        public Player(Game1 game, Vector2 position, Camera camera) : base()
        {
            this.game = (Game1)game;
            this.camera = camera;
            Position = position;
            Speed = 5f;
            LoadContent();
        }

        #endregion

        #region Methods

        public void LoadContent()
        {
            spriteTexture = game.Content.Load<Texture2D>("Sprites/Player/normalAnimation");
            normalAnimation = new Animation(spriteTexture, Position, 64, 78, 4, 50, Color.White, 1f, true);
            CurrentAnimation = normalAnimation;
        }

        public void HandleInput()
        {
            Motion = Vector2.Zero;

            if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterLeft))
                MotionX = -1;
            else if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterRight))
                MotionX = 1;

            if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterUp))
                MotionY = -1;
            else if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterDown))
                MotionY = 1;

            if (Motion != Vector2.Zero)
            {
                Velocity = Motion * Speed;
                Position += Velocity * Speed;
                LockToMap();

                if (camera.Mode == CameraMode.Follow)
                    camera.LockToSprite(this);

            }
        }

        #endregion



        #region Update

        public void Update(GameTime gameTime)
        {
            HandleInput();
            determineCurrentDirection();
            determineRotation();
            CurrentAnimation.Update(gameTime);
        }

        #endregion

    }
}
