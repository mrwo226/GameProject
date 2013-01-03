using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaActionLibrary.SpriteClasses;
using XnaActionLibrary.TileEngine;

namespace WindowsGame1.Projectiles
{
    public class ChargedLaser : Projectile
    {
        Game game;
        Animation chargeAnimation;
        Animation fullPowerAnimation;
        Animation halfPowerAnimation;
        Animation deactivatingAnimation;
        Animation currentAnimation;
        public bool isCharging;
        public bool Deactivating;
        TimeSpan deactivatingTimer = TimeSpan.FromSeconds(0.75);
        TimeSpan currentTimer;

        public override int Width
        {
            get { return currentAnimation.FrameWidth; }
        }
        public override int Height
        {
            get { return currentAnimation.FrameHeight; }
        }

        public ChargedLaser(Game game, ProjectileAlignment alignment) : base()
        {
            this.game = (Game1)game;
            Speed = 15f;
            Alignment = alignment;
            LoadContent();
        }

        private void LoadContent()
        {
            // Load images.
            Texture2D chargeTexture = game.Content.Load<Texture2D>("Weapons/chargeLaser");
            Texture2D fullPowerTexture = game.Content.Load<Texture2D>("Weapons/chargeLaserFullPowerLoop");
            Texture2D halfPowerTexture = game.Content.Load<Texture2D>("Weapons/chargeLaserHalfPowerLoop");
            Texture2D deactivateTexture = game.Content.Load<Texture2D>("Weapons/deactivateCharge");

            // Allocate new animations.
            chargeAnimation = new Animation(chargeTexture, Vector2.Zero, 64, 64, 20, 40, Color.White, 0.7f, false);
            fullPowerAnimation = new Animation(fullPowerTexture, Vector2.Zero, 64, 64, 4, 100, Color.White, 0.7f, true);
            halfPowerAnimation = new Animation(halfPowerTexture, Vector2.Zero, 54, 54, 4, 100, Color.White, 0.6f, true);
            deactivatingAnimation = new Animation(deactivateTexture, Vector2.Zero, 64, 64, 10, 10, Color.White, 0.7f, false);
            currentAnimation = chargeAnimation;           
        }

        public void SetChargeAnimation(Animation animation)
        {
            // If this animation is already running, do not restart it.
            if (currentAnimation == animation)
                return;

            // Start the new animation.
            currentAnimation = animation;
            currentAnimation.currentFrame = 0;
            currentAnimation.elapsedTime = 0.0f;
        }

        public void Charge()
        {
            SetChargeAnimation(chargeAnimation);
            isCharging = true;
        }

        public void Deactivate(GameTime gameTime)
        {
            SetChargeAnimation(deactivatingAnimation);
            Deactivating = true;
            currentTimer = gameTime.TotalGameTime;
        }

        public void FireHalfLaser(float rotation)
        {
            SetChargeAnimation(halfPowerAnimation);
        }

        public void FireFullLaser(float rotation)
        {
            SetChargeAnimation(fullPowerAnimation);
        }

        public void HoldFullLaser()
        {
            SetChargeAnimation(fullPowerAnimation);
        }

        public override void Update(GameTime gameTime)
        {
            currentAnimation.Update(gameTime);
            if (Deactivating)
            {
                if (gameTime.TotalGameTime - currentTimer > deactivatingTimer)
                    IsActive = false;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the charged laser to the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(currentAnimation.spriteStrip, Position - camera.Position, currentAnimation.SourceRectangle, Color.White, Rotation, Origin, 1, SpriteEffects.None, 0);
        }
    }
}
