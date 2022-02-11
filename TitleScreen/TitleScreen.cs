using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TitleScreen
{
    public class TitleScreen : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        private SpriteFont bangers;
        private SaloonSprite saloon;
        private ChestSprite chest;
        private StickmanSprite stickman;
        private BatSprite[] bats;

        public TitleScreen()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            stickman = new StickmanSprite() { Position = new Vector2(100, 340), };
            bats = new BatSprite[]
            {
                new BatSprite(){ Position = new Vector2(320, 250), Horizontal = Direction.Right},
                new BatSprite() { Position = new Vector2(300, 220), Horizontal = Direction.Left},
            };
            chest = new ChestSprite();
            saloon = new SaloonSprite();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            stickman.LoadContent(Content);
            foreach (var bat in bats) bat.LoadContent(Content);
            chest.LoadContent(Content);
            saloon.LoadContent(Content);

            bangers = Content.Load<SpriteFont>("bangers");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            stickman.Update(gameTime);
            foreach (var bat in bats) bat.Update(gameTime);

            chest.Update(gameTime, new Vector2(100, GraphicsDevice.PresentationParameters.Bounds.Height - 64));
            saloon.Update(gameTime, new Vector2(500, GraphicsDevice.PresentationParameters.Bounds.Height - 256));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            saloon.Draw(gameTime, spriteBatch);
            chest.Draw(gameTime, spriteBatch);
            stickman.Draw(gameTime, spriteBatch);
            foreach (var bat in bats) bat.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(bangers, "Wandering Slinger", new Vector2(100, 50), Color.Red, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(bangers, "Press ESC or Back to Exit", new Vector2(300, 200), Color.Red, 0, new Vector2(0, 0), .25f, SpriteEffects.None, 0);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
