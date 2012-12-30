using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaActionLibrary;
using XnaActionLibrary.SpriteClasses;
using XnaActionLibrary.TileEngine;
using WindowsGame1.Projectiles;

namespace WindowsGame1.Sprites
{
    /// <summary>
    /// Defines the main character in the game controlled by the player.  
    /// </summary>
    public class Player : AnimatedSprite
    {
        #region Fields

        Game game; // A reference to the current game type.
        Animation normalAnimation; // The normal/idle animation for this sprite.
        Camera camera; // A reference to the game camera.

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

        /// <summary>
        /// Handles the movement input for the sprite.
        /// </summary>
        public void HandleMovementInput()
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

            // Normalize the velocity for diagonal movement and update the player position.
            if (Motion != Vector2.Zero)
            {
                Velocity = Motion * Speed;
                Position += Velocity * Speed;
                LockToMap();

                if (camera.Mode == CameraMode.Follow)
                    camera.LockToSprite(this);

            }
        }

        /// <summary>
        /// Handles the attack input for the sprite.
        /// </summary>
        public void HandleAttackInput()
        {
            // Basic attack.
            if (InputManager.IsActionTriggered(InputManager.Action.AttackBasicLaser))
                ProjectileManager.Instance.CreateProjectile(ProjectileType.BasicLaser, WeaponSpawnPosition, game, ProjectileAlignment.Friendly);
                
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            HandleMovementInput();
            HandleAttackInput();
            base.Update(gameTime);
        }

        #endregion

    }
}
