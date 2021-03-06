using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using TitleScreen.Levels;
using TitleScreen.Sprites;
using TitleScreen.Sprites.Items;

namespace TitleScreen
{
    public class TitleScreen : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        public StickmanSprite Stickman;
        private Screen currentScreen;

        static SpriteFont bangers;

        GamePadState Previousgps;
        KeyboardState Previouskbs;
        GamePadState gps;
        KeyboardState kbs;

        private SoundEffect backgroundMusicIntro;
        private Song backgroundMusic;
        float countDuration = 0;
        float songTime = 0;

        public TitleScreen()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            ScreenValues.ScreenWidth = GraphicsDevice.PresentationParameters.Bounds.Width;
            ScreenValues.ScreenHeight = GraphicsDevice.PresentationParameters.Bounds.Height;
            Stickman = new StickmanSprite() { Position = new Vector2(100, 340) };
            currentScreen = new Title(spriteBatch, Stickman);
            Pause.LoadContent(Content);
            Lose.LoadContent(Content);
            currentScreen.Initialize();
            currentScreen.LoadContent(Content); ;
            backgroundMusicIntro = Content.Load<SoundEffect>("OpeningPiano");
            backgroundMusic = Content.Load<Song>("BackgroundMusicBody");
            countDuration = 0;//backgroundMusicIntro.Duration.Seconds + .7f;
            MediaPlayer.IsRepeating = false;
            //backgroundMusicIntro.Play();

            bangers = Content.Load<SpriteFont>("bangers");
        }
        public bool EndOfScreen()
        {
            if (Stickman.Position.X + Stickman.pixelWidth - 35 > ScreenValues.ScreenWidth)
            {
                ScreenValues.SickmanSpawnLocation = SpawnLocation.Left;
                Stickman.LoadNewPage();
                return true;
            }
            else if (Stickman.Position.X + 15 < 0)
            {
                ScreenValues.SickmanSpawnLocation = SpawnLocation.Right;
                Stickman.LoadNewPage();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void LoadNewScreen()
        {
            ScreenValues.NewArea();
            switch (ScreenValues.CurrentScreen)
            {
                case ScreenValues.Areas.Blank:
                    currentScreen = new TutorialScreen(spriteBatch, Stickman);
                    currentScreen.Initialize();
                    currentScreen.LoadContent(Content);
                    break;
                case ScreenValues.Areas.Bosses:
                    currentScreen = new OutlawScreen(spriteBatch, Stickman);
                    currentScreen.Initialize();
                    currentScreen.LoadContent(Content);
                    break;
                case ScreenValues.Areas.Chest:
                    currentScreen = new Title(spriteBatch, Stickman);
                    currentScreen.Initialize();
                    currentScreen.LoadContent(Content);
                    break;
                case ScreenValues.Areas.Store:
                    currentScreen = new Title(spriteBatch, Stickman);
                    currentScreen.Initialize();
                    currentScreen.LoadContent(Content);
                    break;
                case ScreenValues.Areas.Trader:
                    currentScreen = new Title(spriteBatch, Stickman);
                    currentScreen.Initialize();
                    currentScreen.LoadContent(Content);
                    break;
                case ScreenValues.Areas.Wall:
                    currentScreen = new Title(spriteBatch, Stickman);
                    currentScreen.Initialize();
                    currentScreen.LoadContent(Content);
                    break;
            }
        }


        private void screenUpdate(GameTime gameTime, KeyboardState KBstate, GamePadState GPstate)
        {
            songTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (songTime >= countDuration && !MediaPlayer.IsRepeating)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(backgroundMusic);
            }
            currentScreen.Update(gameTime, KBstate, GPstate);
        }

        protected override void Update(GameTime gameTime)
        {
            Previousgps = gps;
            Previouskbs = kbs;
            gps = GamePad.GetState(PlayerIndex.One);
            kbs = Keyboard.GetState();

            if (ScreenValues.State == ScreenValues.GameState.TitleScreen)                   //Title Screen
            {
                if (gps.Buttons.Start == ButtonState.Pressed || 
                    gps.Buttons.A == ButtonState.Pressed || 
                    kbs.IsKeyDown(Keys.Enter))                                              //Start
                {
                    LoadNewScreen();
                }
                if ((gps.Buttons.Back == ButtonState.Pressed && Previousgps.Buttons.Back == ButtonState.Released) || (kbs.IsKeyDown(Keys.Escape) && Previouskbs.IsKeyUp(Keys.Escape))) //Exit
                {
                    Exit();
                }
            }
            else if (ScreenValues.State == ScreenValues.GameState.PauseMenu)                //Paused
            {
                if ((gps.Buttons.Start == ButtonState.Pressed && Previousgps.Buttons.Start != ButtonState.Pressed) || (kbs.IsKeyDown(Keys.Enter) && !Previouskbs.IsKeyDown(Keys.Enter)))            //Continue
                {
                    ScreenValues.State = ScreenValues.GameState.Free;
                }
                else if ((gps.Buttons.Back == ButtonState.Pressed && Previousgps.Buttons.Back != ButtonState.Pressed) || (kbs.IsKeyDown(Keys.Escape) && !Previouskbs.IsKeyDown(Keys.Escape)))          //Quit to Main Menu
                {
                    ScreenValues.ResetClass();
                    LoadContent();
                }
            }
            else if (ScreenValues.State == ScreenValues.GameState.DeathScreen)                //Paused
            {
                if ((gps.Buttons.Back == ButtonState.Pressed && Previousgps.Buttons.Back != ButtonState.Pressed) || (kbs.IsKeyDown(Keys.Escape) && !Previouskbs.IsKeyDown(Keys.Escape)))          //Quit to Main Menu
                {
                    ScreenValues.ResetClass();
                    LoadContent();
                }
            }
            else if (ScreenValues.State == ScreenValues.GameState.Tutorial)             //Tutorial
            {

                if ((gps.Buttons.Start == ButtonState.Pressed && Previousgps.Buttons.Start != ButtonState.Pressed) || 
                    (kbs.IsKeyDown(Keys.Enter) && !Previouskbs.IsKeyDown(Keys.Enter)))        //exit Tutorial 
                {
                    ScreenValues.State = ScreenValues.GameState.Free;
                    Stickman.GetGun(Content);
                }
                else if (ScreenValues.tutorial == ScreenValues.Tutorial.PauseIt && ((gps.Buttons.Back == ButtonState.Pressed && Previousgps.Buttons.Back != ButtonState.Pressed) ||
                    (kbs.IsKeyDown(Keys.Escape) && !Previouskbs.IsKeyDown(Keys.Escape))))         //exit Tutorial 
                {
                    ScreenValues.State = ScreenValues.GameState.PauseMenu;
                }
            }
            else if (ScreenValues.State == ScreenValues.GameState.Free)             //FREE
            {

                if ((gps.Buttons.Back == ButtonState.Pressed && Previousgps.Buttons.Back != ButtonState.Pressed) || 
                    (kbs.IsKeyDown(Keys.Escape) && !Previouskbs.IsKeyDown(Keys.Escape)))         //exit Tutorial 
                {
                    ScreenValues.State = ScreenValues.GameState.PauseMenu;
                }
            }

            screenUpdate(gameTime, Keyboard.GetState(), GamePad.GetState(PlayerIndex.One));

            if (EndOfScreen())
            {
                LoadNewScreen();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Matrix transform;
            if (ScreenValues.GameState.DeathScreen == ScreenValues.State)
            {
                Random rand = new Random();
                int xOffset = rand.Next(0, ScreenValues.ScreenWidth / 12);// * (rand.Next(0, 2) * 2 - 1);
                int yOffset = rand.Next(0, ScreenValues.ScreenHeight / 12);// * (rand.Next(0, 2) * 2 - 1);
                //
                //
                //int zOffset = (rand.Next(0, 12) * 2 - 1);
                transform = Matrix.CreateTranslation(xOffset * 0.1f, yOffset * 0.1f, 0);
            }
            else
            {
                transform = Matrix.CreateTranslation(0, 0, 0);
            }

            spriteBatch.Begin(transformMatrix: transform);

            currentScreen.Draw(gameTime);

            if (ScreenValues.State != ScreenValues.GameState.TitleScreen && Stickman.item != null)
            {
                spriteBatch.DrawString(bangers, ((Gun2)Stickman.item).BulletCount.ToString(), new Vector2(10, 10), Color.Gold, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
            }

            spriteBatch.End();


            if (ScreenValues.State == ScreenValues.GameState.PauseMenu)
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive); //Get rid of stuff in this spritebatch.Begin
                Pause.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if (ScreenValues.State == ScreenValues.GameState.DeathScreen)
            {
                spriteBatch.Begin();
                Lose.Draw(spriteBatch);
                spriteBatch.End();
            }
            
            base.Draw(gameTime);
        }
    }
}
