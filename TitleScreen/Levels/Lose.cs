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
    public static class Lose
    {
        static Texture2D pixel;
        static SpriteFont bangers;
        public static void LoadContent(ContentManager Content)
        {
            pixel = Content.Load<Texture2D>("PIXEL");
            bangers = Content.Load<SpriteFont>("bangers");
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, new Rectangle(0, 0, ScreenValues.ScreenWidth, ScreenValues.ScreenHeight), Color.DarkRed * (float).8);
            spriteBatch.DrawString(bangers, "YOU LOSE", new Vector2(200, 50), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(bangers, "Press ESC or BACK to go back to main menu", new Vector2(100, 250), Color.White, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
        }
    }
}
