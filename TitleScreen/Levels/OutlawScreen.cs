using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TitleScreen.Content;
using TitleScreen.Levels;
using TitleScreen.Sprites;
using TitleScreen.Sprites.Items;

namespace TitleScreen.Levels
{
    public class OutlawScreen : Screen
    {
        private BatSprite[] bats;
        //private ChestSprite chest;
        private OutlawSprite outlaw;


        GamePadState Previousgps;
        KeyboardState Previouskbs;

        public OutlawScreen(SpriteBatch spriteBatch, StickmanSprite Stickman)
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
            //chest = new ChestSprite(new Vector2(100, ScreenValues.ScreenHeight - 64 - 20)) { contents = Treasure.Gun };
            outlaw = new OutlawSprite(true);
        }

        public override void LoadContent(ContentManager Content)
        {
            foreach (var bat in bats) bat.LoadContent(Content);
            //chest.LoadContent(Content);
            outlaw.LoadContent(Content);
        }

        public override void Update(GameTime gameTime, KeyboardState KBstate, GamePadState GPstate)
        {
            if (ScreenValues.State != ScreenValues.GameState.PauseMenu && ScreenValues.State != ScreenValues.GameState.DeathScreen)
            {
                stickman.Update(gameTime);
                foreach (var bat in bats) bat.Update(gameTime);

                /*if (chest.content != null)
                {
                    if (stickman.Bounds.CollidesWith(chest.Bounds) && ScreenValues.Tutorial.CollectItems == ScreenValues.tutorial)
                    {
                        if (((KBstate.IsKeyDown(Keys.E) && Previouskbs.IsKeyUp(Keys.E)) || (GPstate.Buttons.X == ButtonState.Pressed && Previousgps.Buttons.X != ButtonState.Pressed)) && chest.animationFrame != 1)
                        {
                            chest.animationFrame = 1;
                            chest.contents = 0;
                            ScreenValues.tutorial = ScreenValues.Tutorial.PickUp;
                        }
                    }
                    else if (stickman.Bounds.CollidesWith(chest.content.Bounds) && ScreenValues.Tutorial.PickUp == ScreenValues.tutorial)
                    {
                        if (((KBstate.IsKeyDown(Keys.E) && Previouskbs.IsKeyUp(Keys.E)) || (GPstate.Buttons.X == ButtonState.Pressed && Previousgps.Buttons.X != ButtonState.Pressed)) && chest.animationFrame == 1)
                        {
                            ScreenValues.tutorial = ScreenValues.Tutorial.Shoot;
                            stickman.item = chest.content;
                            chest.content = null;
                        }
                    }

                }*/

                Gun2 g2 = null;
                Gun2 og2 = null;
                if (stickman.item is Gun2)
                {
                    g2 = (Gun2)stickman.item;
                    foreach (Bullet bullet in g2.bullets)
                    {
                        if (outlaw.animationFrame != 2 && bullet.Visible && bullet.collides(outlaw.Bounds))
                        {
                            outlaw.Hit();
                            bullet.Visible = false;
                        }
                    }
                }
                if (outlaw.item is Gun2 && outlaw.animationFrame != 2)
                {
                    og2 = (Gun2)outlaw.item;

                    foreach (Bullet bullet in og2.bullets)
                    {
                        if (bullet.Visible && bullet.collides(stickman.Bounds))
                        {
                            ScreenValues.State = ScreenValues.GameState.DeathScreen;
                            bullet.Visible = false;
                        }
                    }
                }
                if(og2 != null && g2 != null)
                {
                    foreach (Bullet bullet in g2.bullets)
                    {
                        if (bullet.Visible)
                        {

                            foreach (Bullet badBullet in og2.bullets)
                            {
                                if (badBullet.Visible && badBullet.collides(bullet.Bounds))
                                {
                                    bullet.Visible = false;
                                    badBullet.Visible = false;
                                }
                            }
                        }
                    }
                }
                outlaw.Update(gameTime);
                Previousgps = GPstate;
                Previouskbs = KBstate;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            outlaw.Draw(gameTime, spriteBatch);
            stickman.Draw(gameTime, spriteBatch);
            foreach (var bat in bats) bat.Draw(gameTime, spriteBatch);
            if (ScreenValues.State == ScreenValues.GameState.PauseMenu) Pause.Draw(spriteBatch);
            else if (ScreenValues.State == ScreenValues.GameState.DeathScreen) Lose.Draw(spriteBatch);
        }
    }
}
