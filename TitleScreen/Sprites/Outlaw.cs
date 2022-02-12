using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

using TitleScreen.Collisions;
using TitleScreen.Content;
using TitleScreen.Sprites.Items;

namespace TitleScreen.Sprites
{

    /// <summary>
    /// A class representing a bat sprite
    /// </summary>
    public class OutlawSprite : Sprite
    {
        private double animationTimer;
        public short animationFrame;
        private double shootTimer;

        public bool Visible = false;
        public Direction dir;

        public Bullet bullet;

        private BoundingRectangle bounds;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        public OutlawSprite(Vector2 position)
        {
            Position = position;
            pixelWidth = 105;
            pixelHeight = 255;
            this.bounds = new BoundingRectangle(Position, pixelWidth, pixelHeight);

            bullet = new Bullet();
        }

        /// <summary>
        /// Loads the bat sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Enemy");
        }

        /// <summary>
        /// Updates the bat sprite to fly in a pattern
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (animationTimer > 0.6)
                {
                    shootTimer += gameTime.ElapsedGameTime.TotalSeconds;

                    if(shootTimer > 2)
                    {
                        bullet.dir = (this.dir == Direction.Left) ? Direction.Right : Direction.Left;
                        bullet.Position = (this.dir == Direction.Left) ? Position : new Vector2(Position.X + pixelWidth, Position.Y);
                        bullet.Visible = true;
                    }
                }

            }
        }

        /// <summary>
        /// Draws the animated bat sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The SpriteBatch to draw with</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            
            //Update animation frame
            if (animationTimer > 0.6)
            {
                animationFrame = 1;
            }

            //Draw the sprite
            var source = new Rectangle(animationFrame * pixelWidth, 0, pixelWidth, pixelHeight);
            SpriteEffects spriteEffects = (ScreenValues.SickmanSpawnLocation == SpawnLocation.Left) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, Position, source, Color.White, 0, new Vector2(0, 0), 1, spriteEffects, 0);
        }
    }
}
