using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class TutorialScreen : Screen
    {
        private BatSprite[] bats;
        private ChestSprite chest;
        private OutlawSprite outlaw;


        GamePadState Previousgps;
        KeyboardState Previouskbs;

        public TutorialScreen(SpriteBatch spriteBatch, StickmanSprite Stickman)
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
            chest = new ChestSprite(new Vector2(100, ScreenValues.ScreenHeight - 64 - 20), Treasure.Gun);
            outlaw = new OutlawSprite(new Vector2(ScreenValues.ScreenWidth - 100, ScreenValues.ScreenHeight - 260), SpawnLocation.Left, false);
        }

        public override void LoadContent(ContentManager Content)
        {
            foreach (var bat in bats) bat.LoadContent(Content);
            chest.LoadContent(Content);
            outlaw.LoadContent(Content);
        }

        public void DontLetLeave()
        {
            if (stickman.Position.X + stickman.pixelWidth - 35 > ScreenValues.ScreenWidth)
            {
                stickman.Position.X = stickman.Position.X - 50;
            }
            else if (stickman.Position.X + 15 < 0)
            {
                stickman.Position.X = stickman.Position.X + 15;
            }
        }

        public override void Update(GameTime gameTime, KeyboardState KBstate, GamePadState GPstate)
        {
            if (ScreenValues.State != ScreenValues.GameState.PauseMenu && ScreenValues.State != ScreenValues.GameState.DeathScreen)
            {
                stickman.Update(gameTime);
                foreach (var bat in bats) bat.Update(gameTime);

                if (chest.content != null && ScreenValues.Tutorial.Completed > ScreenValues.tutorial && ScreenValues.Tutorial.NewTerrain < ScreenValues.tutorial)
                {
                    DontLetLeave();

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

                }
                if (ScreenValues.Tutorial.Shoot == ScreenValues.tutorial)
                {
                    DontLetLeave();

                    if ((GPstate.Triggers.Right > 0.75 && !(Previousgps.Triggers.Right > 0.75)) ||
                        (KBstate.IsKeyDown(Keys.Space) && Previouskbs.IsKeyDown(Keys.Space)) && chest.animationFrame == 1)
                    {
                        ScreenValues.tutorial = ScreenValues.Tutorial.Fight;
                        outlaw.Visible = true;
                    }
                }
                else if (ScreenValues.Tutorial.Fight == ScreenValues.tutorial)
                {
                    Gun2 g2 = null;
                    Gun2 og2 = null;
                    if (stickman.item is Gun2)
                    {
                        g2 = (Gun2)stickman.item;
                        foreach (Bullet bullet in g2.bullets)
                        {
                            if (bullet.collides(outlaw.Bounds))
                            {
                                outlaw.animationFrame = 2;
                                bullet.Visible = false;
                                ScreenValues.tutorial = ScreenValues.Tutorial.PauseIt;
                            }
                        }
                    }
                    if (outlaw.item is Gun2)
                    {
                        og2 = (Gun2)outlaw.item;

                        foreach (Bullet bullet in og2.bullets)
                        {
                            if (bullet.collides(stickman.Bounds))
                            {
                                ScreenValues.State = ScreenValues.GameState.DeathScreen;
                                bullet.Visible = false;
                            }
                        }
                    }
                    if (og2 != null && g2 != null)
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
                }
                chest.Update(gameTime);
                outlaw.Update(gameTime);
                Previousgps = GPstate;
                Previouskbs = KBstate;
            }
            else if(ScreenValues.State == ScreenValues.GameState.PauseMenu)
            {
                ScreenValues.tutorial = ScreenValues.Tutorial.Completed;
            }

        }

        public override void Draw(GameTime gameTime)
        {
            if(ScreenValues.GameState.Tutorial == ScreenValues.State)
            {
                spriteBatch.DrawString(bangers, "Press ENTER or Start to Skip", new Vector2(225, 0), Color.Green, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
                if (ScreenValues.Tutorial.Scroll == ScreenValues.tutorial)
                {
                    spriteBatch.DrawString(bangers, "Tutorial:", new Vector2(100, 100), Color.Red, 0, new Vector2(0, 0), .75f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(bangers, "Head in a direction", new Vector2(225, 165), Color.Red, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(bangers, "<->", new Vector2(225, 200), Color.Red, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                }
                else if (ScreenValues.Tutorial.NewTerrain == ScreenValues.tutorial)
                {
                    spriteBatch.DrawString(bangers, "Go back the same way you came", new Vector2(25, 120), Color.Red, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(bangers, "and it will be a different area", new Vector2(25, 165), Color.Red, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
                }
                else if (ScreenValues.Tutorial.CollectItems == ScreenValues.tutorial)
                {
                    spriteBatch.DrawString(bangers, "Interact with objects by pressing 'E' on your keyboard", new Vector2(25, 120), Color.Red, 0, new Vector2(0, 0), .25f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(bangers, " or 'X' on your Controller", new Vector2(25, 170), Color.Red, 0, new Vector2(0, 0), .25f, SpriteEffects.None, 0);
                    chest.Draw(gameTime, spriteBatch);
                }
                else if (ScreenValues.Tutorial.PickUp == ScreenValues.tutorial)
                {
                    spriteBatch.DrawString(bangers, "Pick up Gun with 'E' or 'X'", new Vector2(25, 120), Color.Red, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
                    chest.Draw(gameTime, spriteBatch);
                }
                else if (ScreenValues.Tutorial.Shoot == ScreenValues.tutorial)
                {
                    spriteBatch.DrawString(bangers, "Shoot by pressing the Space bar or Right Trigger", new Vector2(25, 120), Color.Red, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
                    chest.Draw(gameTime, spriteBatch);
                }
                else if (ScreenValues.Tutorial.Fight == ScreenValues.tutorial)
                {
                    spriteBatch.DrawString(bangers, "SHOOT HIM FIRST", new Vector2(100, 50), Color.Red, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                    outlaw.Draw(gameTime, spriteBatch);
                }
                else if (ScreenValues.Tutorial.PauseIt == ScreenValues.tutorial)
                {
                    spriteBatch.DrawString(bangers, "Press 'ESC' or 'Back' to pause", new Vector2(100, 50), Color.Red, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
                    outlaw.Draw(gameTime, spriteBatch);
                }
            }
            else if (ScreenValues.GameState.Free == ScreenValues.State && ScreenValues.Tutorial.Completed == ScreenValues.tutorial)
            {
                spriteBatch.DrawString(bangers, "You have completed the tutorial. Now leave.", new Vector2(25, 120), Color.Red, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
            }

            stickman.Draw(gameTime, spriteBatch);
            foreach (var bat in bats) bat.Draw(gameTime, spriteBatch);
            if (ScreenValues.State == ScreenValues.GameState.PauseMenu) Pause.Draw(spriteBatch);
            else if (ScreenValues.State == ScreenValues.GameState.DeathScreen) Lose.Draw(spriteBatch);
        }
    }
}
