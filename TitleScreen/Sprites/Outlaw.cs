using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        public Item[] dropItems;
        public bool ItemsDropped = false;
        private float gunxdir
        {
            get
            {
                return (dir == Direction.Left) ? Position.X + 5 : Position.X + pixelWidth - 30;
            }
        }
        private float gunydir
        {
            get
            {
                return Position.Y + 160;
            }
        }

        public bool Visible = false;
        public Direction dir;

        public Item item;

        private BoundingRectangle bounds;

        public SpriteEffects spriteEffects;
        private SoundEffect hit;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        public OutlawSprite(bool visible = false)
        {
            Visible = visible;
            spriteEffects = (ScreenValues.SickmanSpawnLocation == SpawnLocation.Left) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            dir = (spriteEffects == SpriteEffects.FlipHorizontally) ? Direction.Left : Direction.Right;

            Position = (dir == Direction.Left) ? new Vector2(ScreenValues.ScreenWidth - 100, ScreenValues.ScreenHeight - 270) : new Vector2(100, ScreenValues.ScreenHeight - 270);
            pixelWidth = 105;
            pixelHeight = 255;
            this.bounds = new BoundingRectangle(Position, pixelWidth, pixelHeight);
            item = new Gun2(new Vector2(gunxdir, gunydir));
            item.falling = false;
            item.spriteEffect = (dir == Direction.Left) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;


        }
        public OutlawSprite(Vector2 position, bool visible = false)
        {
            Visible = visible;
            spriteEffects = (ScreenValues.SickmanSpawnLocation == SpawnLocation.Left) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            dir = (spriteEffects == SpriteEffects.FlipHorizontally) ? Direction.Left : Direction.Right;

            Position = position;
            pixelWidth = 105;
            pixelHeight = 255;
            this.bounds = new BoundingRectangle(Position, pixelWidth, pixelHeight);
            item = new Gun2(new Vector2(gunxdir, gunydir));
            item.falling = false;
            item.spriteEffect = (dir == Direction.Left) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

        }
        public OutlawSprite(Vector2 position, SpawnLocation sl, bool visible = false)
        {
            Visible = visible;
            ScreenValues.SickmanSpawnLocation = sl;
            spriteEffects = (ScreenValues.SickmanSpawnLocation == SpawnLocation.Left) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            dir = (spriteEffects == SpriteEffects.FlipHorizontally) ? Direction.Left : Direction.Right;

            Position = position;
            pixelWidth = 105;
            pixelHeight = 255;
            this.bounds = new BoundingRectangle(Position, pixelWidth, pixelHeight);
            item = new Gun2(new Vector2(gunxdir, gunydir));
            item.falling = false;
            item.spriteEffect = (dir == Direction.Left) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

        }

        /// <summary>
        /// Loads the bat sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Enemy");
            item.LoadContent(content);
            shootTimer = 0;

            hit = content.Load<SoundEffect>("Hit_Hurt");

            dropItems = new Item[]
            {
                new Bullet() {Visible = false, falling = true, shoot = false },
                new Bullet() {Visible = false, falling = true, shoot = false },
                new Bullet() {Visible = false, falling = true, shoot = false }
            };
            foreach(Item i in dropItems)
            {
                i.LoadContent(content);
            }
        }

        public void Hit()
        {
            animationFrame = 2;
            hit.Play();
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
                        //item.Position = new Vector2(xdir, Position.Y + 160);
                        item.spriteEffect = (dir == Direction.Left) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                        item.Update(gameTime);
                    }
                }
                if (animationFrame == 2)
                {
                    foreach (Item i in dropItems)
                    {
                        //i.Draw(gameTime, spriteBatch);
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
            if (Visible)
            {
                //Update animation timer
                animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

                //Draw the sprite
                var source = new Rectangle(animationFrame * pixelWidth, 0, pixelWidth, pixelHeight);
                spriteBatch.Draw(texture, Position, source, Color.White, 0, new Vector2(0, 0), 1, spriteEffects, 0);

                //Update animation frame
                if (animationTimer > 0.6 && animationFrame != 2)
                {
                    animationFrame = 1;
                    item.Draw(gameTime, spriteBatch);
                }

                if(animationFrame == 2 && !ItemsDropped)
                {
                    ItemsDropped = true;
                    foreach (Item i in dropItems)
                    {
                        if(i is Bullet g2)
                        {
                            g2.Visible = true;
                            g2.Spawn();
                            //i.Draw(gameTime, spriteBatch);
                        }
                    }
                }
                else if(animationFrame == 2)
                {
                    foreach (Item i in dropItems)
                    {
                        //i.Draw(gameTime, spriteBatch);
                    }
                }
            }
        }
    }
}
