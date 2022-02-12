using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace TitleScreen.Sprites
{
    public enum Treasure 
    { 
        Gun = 0
    }
    /// <summary>
    /// A class representing a Chest sprite
    /// </summary>
    /// 
    public class ChestSprite
    {
        private Texture2D texture;
        private Treasure contents;

        /// <summary>
        /// The position of the Chest
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Loads the Chest sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Chest");
        }

        /// <summary>
        /// Updates the Chest sprite to fly in a pattern
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime, Vector2 position)
        {
            Position = position;
        }

        /// <summary>
        /// Draws the animated Chest sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The SpriteBatch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, new Rectangle(0, 0, 64, 64), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        }
    }
}
