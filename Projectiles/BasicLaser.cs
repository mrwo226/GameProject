using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1.Projectiles
{
    /// <summary>
    /// Defines a basic projectile for sprites in the game.  A color property can be defined to spawn either an enemy laser texture or a friendly laser texture.
    /// </summary>
    public class BasicLaser : Projectile
    {
        Game game;

        public BasicLaser(Game game, Vector2 position, ProjectileAlignment alignment) : base()
        {
            this.game = (Game1)game;
            Position = position;
            Speed = 15f;
            Alignment = alignment;
            LoadContent();
        }

        public void LoadContent()
        {
            // Loads either an enemy laser or a friendly laser texture depending on who shot it.
            if (Alignment == ProjectileAlignment.Friendly)
                projectileTexture = game.Content.Load<Texture2D>("Weapons/playersLaser");
            if (Alignment == ProjectileAlignment.Enemy)
                projectileTexture = game.Content.Load<Texture2D>("Weapons/enemyLaser");
        }
    }
}
