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
    public class Title : Screen
    {
        private SpriteFont bangers;
        private SaloonSprite saloon;
        private ChestSprite chest;
        private BatSprite[] bats;

        public Title()
        {

        }
        public Title(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
            StickmanSprite.Position = new Vector2(100, 340);
            bats = new BatSprite[]
            {
                new BatSprite(){ Position = new Vector2(320, 250), Horizontal = Direction.Right},
                new BatSprite() { Position = new Vector2(300, 220), Horizontal = Direction.Left},
            };
            chest = new ChestSprite();
            saloon = new SaloonSprite();
        }
        public override void LoadContent(ContentManager Content)
        {
            StickmanSprite.LoadContent(Content);
            foreach (var bat in bats) bat.LoadContent(Content);
            chest.LoadContent(Content);
            saloon.LoadContent(Content);

            bangers = Content.Load<SpriteFont>("bangers");
        }
        public override void Update(GameTime gameTime, KeyboardState KBstate, GamePadState GPstate)
        {
            StickmanSprite.Update(gameTime);
            foreach (var bat in bats) bat.Update(gameTime);

            chest.Update(gameTime, new Vector2(100, ScreenValues.ScreenHeight - 64));
            saloon.Update(gameTime, new Vector2(500, ScreenValues.ScreenHeight - 256));
        }
        public override void Draw(GameTime gameTime)
        {
            saloon.Draw(gameTime, spriteBatch);
            chest.Draw(gameTime, spriteBatch);
            StickmanSprite.Draw(gameTime, spriteBatch);
            foreach (var bat in bats) bat.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(bangers, "Wandering Slinger", new Vector2(100, 50), Color.Red, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(bangers, "Press ENTER or A to Start Adventure!", new Vector2(225, 165), Color.Red, 0, new Vector2(0, 0), .3f, SpriteEffects.None, 0);
            spriteBatch.DrawString(bangers, "Press ESC or Back to Exit", new Vector2(300, 200), Color.Red, 0, new Vector2(0, 0), .25f, SpriteEffects.None, 0);

        }

    }
}
