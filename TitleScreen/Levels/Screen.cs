using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using TitleScreen.Levels;
using TitleScreen.Sprites;

namespace TitleScreen.Levels
{
    public abstract class Screen
    {
        public SpriteBatch spriteBatch { get; set; }
        protected StickmanSprite stickman;
        protected static SpriteFont bangers;
        protected static Song backgroundMusic;
        public abstract void Initialize();
        public abstract void LoadContent(ContentManager Content);
        public abstract void Update(GameTime gameTime, KeyboardState KBstate, GamePadState GPstate);
        public abstract void Draw(GameTime gameTime);
    }
}
