using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TitleScreen.Levels;
using TitleScreen.Sprites;

namespace TitleScreen.Levels
{
    public class Blank : Screen
    {
        public Blank()
        {

        }
        public Blank(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }
        public override void Draw(GameTime gameTime)
        {

        }

        public override void Initialize()
        {

        }

        public override void LoadContent(ContentManager Content)
        {

        }

        public override void Update(GameTime gameTime, KeyboardState KBstate, GamePadState GPstate)
        {
            if(ScreenValues.State == ScreenValues.GameState.PauseMenu) Pause.Draw(spriteBatch);
        }
    }
}
