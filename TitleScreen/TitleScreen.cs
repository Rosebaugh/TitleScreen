using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        GamePadState Previousgps;
        KeyboardState Previouskbs;
        GamePadState gps;
        KeyboardState kbs;

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
            currentScreen.Initialize();
            currentScreen.LoadContent(Content);
        }
        public bool EndOfScreen()
        {
            if (Stickman.Position.X + Stickman.pixelWidth - 15 > ScreenValues.ScreenWidth)
            {
                ScreenValues.SickmanSpawnLocation = SpawnLocation.Left;
                return true;
            }
            else if (Stickman.Position.X - 15 < 0)
            {
                ScreenValues.SickmanSpawnLocation = SpawnLocation.Right;
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
                    currentScreen = new Blank(spriteBatch, Stickman);
                    currentScreen.Initialize();
                    currentScreen.LoadContent(Content);
                    break;
                case ScreenValues.Areas.Bosses:
                    currentScreen = new Title(spriteBatch, Stickman);
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
                if (gps.Buttons.Back == ButtonState.Pressed || kbs.IsKeyDown(Keys.Escape)) //Exit
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
            else if (ScreenValues.State == ScreenValues.GameState.Tutorial)             //Tutorial
            {

                if ((gps.Buttons.Start == ButtonState.Pressed && Previousgps.Buttons.Start != ButtonState.Pressed) || 
                    (kbs.IsKeyDown(Keys.Enter) && !Previouskbs.IsKeyDown(Keys.Enter)))        //exit Tutorial 
                {
                    ScreenValues.State = ScreenValues.GameState.Free;
                    Stickman.item = new Gun2(new Vector2(0, 0));
                    Stickman.item.falling = false;
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


            if (EndOfScreen())
            {
                LoadNewScreen();
            }

            screenUpdate(gameTime, Keyboard.GetState(), GamePad.GetState(PlayerIndex.One));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            currentScreen.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
