using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    public class BasicLaser : Projectile
    {
        Game game;

        public BasicLaser(Game game, Vector2 position) : base()
        {
            this.game = (Game1)game;
            Position = position;
            Speed = 15f;
            LoadContent();
            
        }

        public void LoadContent()
        {
            projectileTexture = game.Content.Load<Texture2D>("Weapons/playersLaser");
        }
    }
}
