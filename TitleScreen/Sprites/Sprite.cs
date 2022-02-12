using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TitleScreen.Sprites
{
    public abstract class Sprite
    {
        /// <summary>
        /// The position of the sprite
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Loads the sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public abstract void LoadContent(ContentManager content);

        /// <summary>
        /// Updates the sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The SpriteBatch to draw with</param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
