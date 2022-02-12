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
    public abstract class Screen
    {
        public SpriteBatch spriteBatch { get; set; }
        public abstract void Initialize();
        public abstract void LoadContent(ContentManager Content);
        public abstract void Update(GameTime gameTime, KeyboardState KBstate, GamePadState GPstate);
        public abstract void Draw(GameTime gameTime);
    }
}
