using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TitleScreen.Levels;
using TitleScreen.Sprites;

namespace TitleScreen.Levels
{
    public class Title : Screen
    {
        private SaloonSprite saloon;
        private ChestSprite chest;
        private BatSprite[] bats;

        public Title()
        {

        }
        public Title(SpriteBatch spriteBatch, StickmanSprite Stickman)
        {
            this.spriteBatch = spriteBatch;
            this.stickman = Stickman;
        }

        public override void Initialize()
        {
            bats = new BatSprite[]
            {
                new BatSprite(){ Position = new Vector2(320, 250), Horizontal = Direction.Right},
                new BatSprite() { Position = new Vector2(300, 220), Horizontal = Direction.Left},
            };
            chest = new ChestSprite(new Vector2(100, ScreenValues.ScreenHeight - 64));
            saloon = new SaloonSprite(new Vector2(500, ScreenValues.ScreenHeight - 256));
        }
        public override void LoadContent(ContentManager Content)
        {
            stickman.LoadContent(Content);
            foreach (var bat in bats) bat.LoadContent(Content);
            chest.LoadContent(Content);
            saloon.LoadContent(Content);

            /*
            backgroundMusic = Content.Load<Song>("DeeYan-Key-TheGame");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
            */
            
            bangers = Content.Load<SpriteFont>("bangers");
        }
        public override void Update(GameTime gameTime, KeyboardState KBstate, GamePadState GPstate)
        {
            stickman.Update(gameTime);
            foreach (var bat in bats) bat.Update(gameTime);

            chest.Update(gameTime);
            saloon.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            saloon.Draw(gameTime, spriteBatch);
            chest.Draw(gameTime, spriteBatch);
            stickman.Draw(gameTime, spriteBatch);
            foreach (var bat in bats) bat.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(bangers, "Wandering Slinger", new Vector2(100, 50), Color.Red, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(bangers, "Press ENTER or A to Start Adventure!", new Vector2(225, 165), Color.Red, 0, new Vector2(0, 0), .3f, SpriteEffects.None, 0);
            spriteBatch.DrawString(bangers, "Press ESC or Back to Exit", new Vector2(300, 200), Color.Red, 0, new Vector2(0, 0), .25f, SpriteEffects.None, 0);

        }

    }
}
