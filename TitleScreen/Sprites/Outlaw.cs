﻿using Microsoft.Xna.Framework;
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

        public Item item;

        private BoundingRectangle bounds;

        public SpriteEffects spriteEffects;

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
            float xdir = (dir == Direction.Left) ? Position.X - 10 : Position.X + pixelWidth - 10;
            item = new Gun2(new Vector2(xdir, Position.Y + 160));
            item.falling = false;
            item.spriteEffect = (dir == Direction.Left) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            spriteEffects = (ScreenValues.SickmanSpawnLocation == SpawnLocation.Left) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        }

        /// <summary>
        /// Loads the bat sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Enemy");
            item.LoadContent(content);
        }

        /// <summary>
        /// Updates the bat sprite to fly in a pattern
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            if (ScreenValues.State != ScreenValues.GameState.PauseMenu)
            {
                if (Visible)
                {
                    if (animationFrame == 1)
                    {
                        shootTimer += gameTime.ElapsedGameTime.TotalSeconds;

                        if(shootTimer > 2)
                        {
                            /*
                            bullet.dir = (this.dir == Direction.Left) ? Direction.Right : Direction.Left;
                            bullet.Position = (this.dir == Direction.Left) ? Position : new Vector2(Position.X + pixelWidth, Position.Y);
                            bullet.Visible = true;
                        */
                            if(item is Gun2 g2)
                            {
                                g2.Shoot();
                                shootTimer = 0;
                            }
                        }
                    }

                    dir = (spriteEffects == SpriteEffects.FlipHorizontally) ? Direction.Left : Direction.Right;
                    if (item != null)
                    {
                        float xdir = (dir == Direction.Left) ? Position.X - 10 : Position.X + pixelWidth - 10;
                        item.Position = new Vector2(xdir, Position.Y + 160);
                        item.spriteEffect = (dir == Direction.Left) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                        item.Update(gameTime);
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
            if (animationTimer > 0.6 && animationFrame != 2)
            {
                animationFrame = 1;
                item.Draw(gameTime, spriteBatch);
            }

            //Draw the sprite
            var source = new Rectangle(animationFrame * pixelWidth, 0, pixelWidth, pixelHeight);
            spriteBatch.Draw(texture, Position, source, Color.White, 0, new Vector2(0, 0), 1, spriteEffects, 0);
        }
    }
}
