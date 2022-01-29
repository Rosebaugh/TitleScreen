using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TitleScreen
{
    public class TitleScreen : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        private StickmanSprite stickman;
        private BatSprite[] bats;
        private SpriteFont bangers;
        private Texture2D saloon;
        private Texture2D chest;

        public TitleScreen()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            stickman = new StickmanSprite() { Position = new Vector2(100, 340), };
            bats = new BatSprite[]
            {
                new BatSprite(){ Position = new Vector2(320, 250), Horizontal = Direction.Right},
                new BatSprite() { Position = new Vector2(300, 220), Horizontal = Direction.Left},
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            stickman.LoadContent(Content);
            foreach (var bat in bats) bat.LoadContent(Content);
            saloon = Content.Load<Texture2D>("Pixel Saloon");
            chest = Content.Load<Texture2D>("Chest");

            bangers = Content.Load<SpriteFont>("bangers");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            stickman.Update(gameTime);
            foreach (var bat in bats) bat.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(saloon, new Vector2(500, GraphicsDevice.PresentationParameters.Bounds.Height - 256), new Rectangle(0, 0, 256, 256), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            spriteBatch.Draw(chest, new Vector2(100, GraphicsDevice.PresentationParameters.Bounds.Height - 64), new Rectangle(0, 0, 64, 64), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

            stickman.Draw(gameTime, spriteBatch);
            foreach (var bat in bats) bat.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(bangers, "Wandering Slinger", new Vector2(100, 50), Color.Red, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(bangers, "Press ESC or Back to Exit", new Vector2(300, 200), Color.Red, 0, new Vector2(0, 0), .25f, SpriteEffects.None, 0);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
