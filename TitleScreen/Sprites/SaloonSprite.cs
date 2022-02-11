﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace TitleScreen
{
    /// <summary>
    /// A class representing a Chest sprite
    /// </summary>
    /// 
    public class SaloonSprite
    {
        private Texture2D texture;
        private Treasure contents;

        /// <summary>
        /// The position of the Saloon
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Loads the Chest sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Pixel Saloon");
        }

        /// <summary>
        /// Updates the Saloon sprite to fly in a pattern
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime, Vector2 position)
        {
            Position = position;
        }

        /// <summary>
        /// Draws the animated Saloon sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The SpriteBatch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, new Rectangle(0, 0, 256, 256), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        }
    }
}
