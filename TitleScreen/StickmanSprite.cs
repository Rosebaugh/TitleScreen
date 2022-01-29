using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TitleScreen
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
    public class StickmanSprite
    {
        private Texture2D texture;
        private double directionTimer;
        private double animationTimer;
        private short animationFrame;

        /// <summary>
        /// The direction of the Man
        /// </summary>
        public ManDirection Direction;

        /// <summary>
        /// The Animation Frame of the Man
        /// </summary>
        public AnimationFrame AnimationFrame;

        /// <summary>
        /// The position of the Man
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Loads the Stickman sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Man");
        }

        /// <summary>
        /// Updates the Man sprite to fly in a pattern
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            //Update the Direction Timer

            //Switch directions every 2 seconds
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if(directionTimer > 5.0)
            {
                switch (Direction)
                {
                    case ManDirection.Right:
                        Direction = ManDirection.Still;
                        AnimationFrame = AnimationFrame.Front;
                        directionTimer += 3;
                        break;
                    case ManDirection.Still:
                        if(AnimationFrame.Front == AnimationFrame)Direction = ManDirection.Left;
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

        /// <summary>
        /// Draws the animated Man sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteManch">The SpriteManch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spritebatch)
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
            var source = new Rectangle(animationFrame * 96, 0, 96, 129);
            SpriteEffects spriteEffects = (Direction == ManDirection.Left) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spritebatch.Draw(texture, Position, source, Color.Black, 0, new Vector2(0, 0), 1, spriteEffects, 0);
        }
    }
}
