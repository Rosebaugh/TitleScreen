using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TitleScreen.Levels;
using TitleScreen.Sprites;

namespace TitleScreen
{
    public class TitleScreen : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        private Screen currentScreen;

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
            currentScreen = new Title(spriteBatch);
            Pause.LoadContent(Content);
            currentScreen.Initialize();
            currentScreen.LoadContent(Content);

            ScreenValues.ScreenWidth = GraphicsDevice.PresentationParameters.Bounds.Width;
            ScreenValues.ScreenHeight = GraphicsDevice.PresentationParameters.Bounds.Height;
        }

        public void LoadNewScreen()
        {
            ScreenValues.NewArea();
            switch (ScreenValues.CurrentScreen)
            {
                case ScreenValues.Areas.Blank:
                    currentScreen = new Title(spriteBatch);
                    currentScreen.Initialize();
                    currentScreen.LoadContent(Content);
                    break;
                case ScreenValues.Areas.Bosses:
                    currentScreen = new Title(spriteBatch);
                    currentScreen.Initialize();
                    currentScreen.LoadContent(Content);
                    break;
                case ScreenValues.Areas.Chest:
                    currentScreen = new Title(spriteBatch);
                    currentScreen.Initialize();
                    currentScreen.LoadContent(Content);
                    break;
                case ScreenValues.Areas.Store:
                    currentScreen = new Title(spriteBatch);
                    currentScreen.Initialize();
                    currentScreen.LoadContent(Content);
                    break;
                case ScreenValues.Areas.Trader:
                    currentScreen = new Title(spriteBatch);
                    currentScreen.Initialize();
                    currentScreen.LoadContent(Content);
                    break;
                case ScreenValues.Areas.Wall:
                    currentScreen = new Title(spriteBatch);
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
            GamePadState gps = GamePad.GetState(PlayerIndex.One);
            KeyboardState kbs = Keyboard.GetState();

            if (ScreenValues.State == ScreenValues.GameState.TitleScreen)                   //Title Screen
            {
                if (gps.Buttons.Start == ButtonState.Pressed || 
                    gps.Buttons.A == ButtonState.Pressed || 
                    kbs.IsKeyDown(Keys.Enter))                                              //Start
                {
                    ScreenValues.State = ScreenValues.GameState.Tutorial;
                }
                if (gps.Buttons.Back == ButtonState.Pressed || kbs.IsKeyDown(Keys.Escape)) //Exit
                {
                    Exit();
                }
            }
            else if (ScreenValues.State == ScreenValues.GameState.PauseMenu)                //Paused
            {
                if (gps.Buttons.Start == ButtonState.Pressed || kbs.IsKeyDown(Keys.Enter))             //Continue
                {
                    ScreenValues.State = ScreenValues.GameState.Free;
                }
                else if (gps.Buttons.Back == ButtonState.Pressed || kbs.IsKeyDown(Keys.Escape))          //Quit to Main Menu
                {
                    ScreenValues.ResetClass();
                    LoadContent();
                }
            }
            else if (ScreenValues.State == ScreenValues.GameState.Tutorial)             //Tutorial
            {

                if (gps.Buttons.Start == ButtonState.Pressed || kbs.IsKeyDown(Keys.Enter))         //exit Tutorial 
                {
                    ScreenValues.State = ScreenValues.GameState.Free;
                }
            }
            else if (ScreenValues.State == ScreenValues.GameState.Free)             //Tutorial
            {

                if (gps.Buttons.Back == ButtonState.Pressed || kbs.IsKeyDown(Keys.Escape))         //exit Tutorial 
                {
                    ScreenValues.State = ScreenValues.GameState.PauseMenu;
                }
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
