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
    public class Bosses : Screen
    {
        private BatSprite[] bats;
        public Bosses(SpriteBatch spriteBatch, StickmanSprite Stickman)
        {
            this.spriteBatch = spriteBatch;
            stickman = Stickman;

            switch (ScreenValues.SickmanSpawnLocation)
            {
                case SpawnLocation.Center:
                    stickman.Position = new Vector2((ScreenValues.ScreenWidth - stickman.pixelWidth) / 2, stickman.Position.Y);
                    break;
                case SpawnLocation.Left:
                    stickman.Position = new Vector2(20, stickman.Position.Y);
                    break;
                case SpawnLocation.Right:
                    stickman.Position = new Vector2(ScreenValues.ScreenWidth - stickman.pixelWidth - 20, stickman.Position.Y);
                    break;
            }
        }

        public override void Initialize()
        {
            bats = new BatSprite[]
            {
                new BatSprite(){ Position = new Vector2(320, 150), Horizontal = Direction.Right},
                new BatSprite() { Position = new Vector2(300, 120), Horizontal = Direction.Left},
                new BatSprite() { Position = new Vector2(650, 150), Horizontal = Direction.Left},
            };
        }

        public override void LoadContent(ContentManager Content)
        {
            foreach (var bat in bats) bat.LoadContent(Content);
        }

        public override void Update(GameTime gameTime, KeyboardState KBstate, GamePadState GPstate)
        {
            if(ScreenValues.State != ScreenValues.GameState.PauseMenu)
            {
                stickman.Update(gameTime);
                foreach (var bat in bats) bat.Update(gameTime);
            }
        }

        public bool EndOfScreen()
        {
            if (stickman.Position.X + stickman.pixelWidth - 15 > ScreenValues.ScreenWidth)
            {
                ScreenValues.SickmanSpawnLocation = SpawnLocation.Left;
                return true;
            }
            else if (stickman.Position.X - 15 < 0)
            {
                ScreenValues.SickmanSpawnLocation = SpawnLocation.Right;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if(ScreenValues.GameState.Tutorial == ScreenValues.State)
            {
                spriteBatch.DrawString(bangers, "Tutorial:", new Vector2(100, 50), Color.Red, 0, new Vector2(0, 0), .75f, SpriteEffects.None, 0);
                spriteBatch.DrawString(bangers, "Press ENTER or Start to Skip", new Vector2(225, 120), Color.Red, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
                spriteBatch.DrawString(bangers, "Head in a direction", new Vector2(225, 165), Color.Red, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
                spriteBatch.DrawString(bangers, "<->", new Vector2(225, 200), Color.Red, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            }

            stickman.Draw(gameTime, spriteBatch);
            foreach (var bat in bats) bat.Draw(gameTime, spriteBatch);
            if (ScreenValues.State == ScreenValues.GameState.PauseMenu) Pause.Draw(spriteBatch);
        }
    }
}
