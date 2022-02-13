using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using TitleScreen.Collisions;
using TitleScreen.Sprites.Items;

namespace TitleScreen.Sprites
{
    public enum ManDirection
    {
        Right = 0,
        Left = 1,
        Still = 2,
        /*
        Up = 2,
        Down = 0,
        Left = 3,
        Right = 1
        */
    }
    public enum AnimationFrame
    {
        Front = 0,
        Step1 = 1,
        Step2 = 2,
        Back = 3
        /*
        Up = 2,
        Down = 0,
        Left = 3,
        Right = 1
        */
    }

    /// <summary>
    /// A class representing a Man sprite
    /// </summary>
    public class StickmanSprite : Sprite
    {

        private double directionTimer;
        private double animationTimer;
        private short animationFrame;

        public Item item;

        /// <summary>
        /// The direction of the Man
        /// </summary>
        public ManDirection Direction;
        SpriteEffects spriteEffects;

        /// <summary>
        /// The Animation Frame of the Man
        /// </summary>
        public AnimationFrame AnimationFrame;


        GamePadState Previousgps;
        KeyboardState Previouskbs;
        GamePadState gps;
        KeyboardState kbs;

        private BoundingRectangle bounds;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        public StickmanSprite()
        {
            pixelWidth = 96;
            pixelHeight = 129;
            item = null;
        }

        /// <summary>
        /// Loads the Stickman sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Man");
            this.bounds = new BoundingRectangle(Position.X + (pixelWidth/2) - 5, Position.Y, 10, pixelHeight);
        }

        /// <summary>
        /// Updates the Man sprite to fly in a pattern
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            //Update the Direction Timer
            bounds.X = Position.X + (pixelWidth / 2) - 5;
            bounds.Y = Position.Y;

            if (ScreenValues.State == ScreenValues.GameState.TitleScreen)
            {
                //Switch directions every 2 seconds
                directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

                if (directionTimer > 5.0)
                {
                    switch (Direction)
                    {
                        case ManDirection.Right:
                            Direction = ManDirection.Still;
                            AnimationFrame = AnimationFrame.Front;
                            directionTimer += 3;
                            break;
                        case ManDirection.Still:
                            if (AnimationFrame.Front == AnimationFrame) Direction = ManDirection.Left;
                            else Direction = ManDirection.Right;
                            break;
                        case ManDirection.Left:
                            Direction = ManDirection.Still;
                            AnimationFrame = AnimationFrame.Back;
                            directionTimer += 3;
                            break;


                    }
                    directionTimer -= 5;
                }
                //Move the Man in the direction it is flying

                switch (Direction)
                {
                    case ManDirection.Right:
                        Position += new Vector2(1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case ManDirection.Still:
                        break;
                    case ManDirection.Left:
                        Position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                }
            }
            else if (ScreenValues.State != ScreenValues.GameState.PauseMenu)
            {
                KeyboardState KBstate = Keyboard.GetState();
                GamePadState GPstate = GamePad.GetState(PlayerIndex.One);
                Direction = ManDirection.Still;
                if (KBstate.IsKeyDown(Keys.Right) || KBstate.IsKeyDown(Keys.D))
                {
                    Position += new Vector2(1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = ManDirection.Right;
                }
                if (KBstate.IsKeyDown(Keys.Left) || KBstate.IsKeyDown(Keys.A))
                {
                    Position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = ManDirection.Left;
                }
                if (KBstate.IsKeyDown(Keys.Up) || KBstate.IsKeyDown(Keys.W))
                {
                    
                }
                if (KBstate.IsKeyDown(Keys.Down) || KBstate.IsKeyDown(Keys.S))
                {
                    
                }

                if (GPstate.ThumbSticks.Left.X < 0)
                {
                    Position += new Vector2(1, 0) * 100 * GPstate.ThumbSticks.Left.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = ManDirection.Left;
                }
                else if (GPstate.ThumbSticks.Left.X > 0)
                {
                    Position += new Vector2(1, 0) * 100 * GPstate.ThumbSticks.Left.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = ManDirection.Right;
                }

            }

            if(item != null && Direction != ManDirection.Still)
            {
                float xdir = (Direction == ManDirection.Left) ? Position.X : Position.X + pixelWidth - 20;
                item.Position = new Vector2(xdir, Position.Y + 60);
            }

            if(item != null)
            {

                Previousgps = gps;
                Previouskbs = kbs;
                gps = GamePad.GetState(PlayerIndex.One);
                kbs = Keyboard.GetState();

                if ((gps.Triggers.Right > 0.75 && !(Previousgps.Triggers.Right > 0.75)) ||
                    (kbs.IsKeyDown(Keys.Space) && Previouskbs.IsKeyUp(Keys.Space)))
                {
                    if(item is Gun2 g2)
                    {
                        g2.Shoot();
                    }
                }
                item.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws the animated Man sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteManch">The SpriteManch to draw with</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Update animation frame
            if (ManDirection.Still != Direction)
            {
                //Update animation timer
                animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (animationTimer > 0.3)
                {
                    animationFrame++;
                    animationTimer -= .3;
                    if(animationFrame > 2) animationFrame = 1;
                }

                //Draw the sprite
            }
            else
            {
                animationTimer = 1;
                animationFrame = (short)AnimationFrame;
            }
            var source = new Rectangle(animationFrame * pixelWidth, 0, pixelWidth, pixelHeight);

            if(Direction != ManDirection.Still)
            {
                spriteEffects = (Direction == ManDirection.Left) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            }
            spriteBatch.Draw(texture, Position, source, Color.Black, 0, new Vector2(0, 0), 1, spriteEffects, 0);

            if (item != null)
            {
                this.item.spriteEffect = (spriteEffects == SpriteEffects.None) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                item.Draw(gameTime, spriteBatch);
            }
        }
    }
}
