using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using TitleScreen.Collisions;
using TitleScreen.Sprites.Items;

namespace TitleScreen.Sprites
{
    /// <summary>
    /// A class representing a Chest sprite
    /// </summary>
    /// 
    public class ChestSprite : Sprite
    {
        public Treasure contents;
        public Item content;

        private BoundingRectangle bounds;

        public short animationFrame;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        public ChestSprite(Vector2 position)
        {
            animationFrame = 0;
            Position = position;
            pixelWidth = 64;
            pixelHeight = 64;
            this.bounds = new BoundingRectangle(position, pixelWidth, pixelHeight);

            content = new Gun2(new Vector2(Position.X + pixelWidth/2, Position.Y + 35));
        }

        /// <summary>
        /// Loads the Chest sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Chest");
            this.content.LoadContent(content);
        }

        /// <summary>
        /// Updates the Chest sprite to fly in a pattern
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            if(animationFrame == 1 && contents == Treasure.Empty && content != null)
            {
                content.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws the animated Chest sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The SpriteBatch to draw with</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var source = new Rectangle(animationFrame * pixelWidth, 0, pixelWidth, pixelHeight);
            spriteBatch.Draw(texture, Position, source, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

            if (animationFrame == 1 && contents == Treasure.Empty && content != null)
            {
                content.Draw(gameTime, spriteBatch);
            }
        }
    }
}
