using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using TitleScreen.Collisions;

namespace TitleScreen.Sprites.Items
{
    public class Gun2 : Item
    {
        public Gun2(Vector2 position)
        {
            Position = position;
            pixelWidth = 32;
            pixelHeight = 25;
            spriteEffect = SpriteEffects.None;

            this.bounds = new BoundingCircle(position + new Vector2((pixelWidth + 8) /2, (pixelWidth + 8) / 2), (pixelWidth + 8) / 2);
            Spawn();
        }
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Gun2");
        }

        public override void Update(GameTime gameTime)
        {
            Position = this.updateFallVector(gameTime, Position);
            bounds.Center = Position + new Vector2((pixelWidth + 8) / 2, (pixelWidth + 8) / 2);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var source = new Rectangle(0, 0, pixelWidth, pixelHeight);
            spriteBatch.Draw(texture, Position, source, Color.White, 0, new Vector2(0, 0), 1, spriteEffect, 0);

        }

    }
}
