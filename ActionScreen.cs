using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class ActionScreen : GameScreen
    {
        public ActionScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.IsActionTriggered(InputManager.Action.ExitGame))
                game.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
