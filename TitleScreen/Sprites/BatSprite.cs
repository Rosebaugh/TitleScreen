using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace TitleScreen
{
    public enum Direction
    {
        Down = 0,
        Right = 1,
        Up = 2,
        Left = 3
        /*
        Up = 2,
        Down = 0,
        Left = 3,
        Right = 1
        */
    }

    /// <summary>
    /// A class representing a bat sprite
    /// </summary>
    public class BatSprite
    {
        private Texture2D texture;
        private double hDirectionTimer;
        private double vDirectionTimer;
        private double animationTimer;
        private short animationFrame;

        /// <summary>
        /// The direction of the bat
        /// </summary>
        public Direction Horizontal;
        public Direction Veritcle;

        /// <summary>
        /// The position of the bat
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Loads the bat sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("32x32-bat-sprite");
        }

        /// <summary>
        /// Updates the bat sprite to fly in a pattern
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            //Update the Direction Timer

            //Switch directions every 2 seconds
            vDirectionTimer += gameTime.ElapsedGameTime.TotalSeconds;
            hDirectionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (hDirectionTimer > 3.0)
            {
                switch (Horizontal)
                {
                    //    case Direction.Up:
                    //        Direction = Direction.Down;
                    //        break;
                    //    case Direction.Down:
                    //        Direction = Direction.Right;
                    //        break;
                    case Direction.Right:
                        Horizontal = Direction.Left;
                        break;
                    case Direction.Left:
                        Horizontal = Direction.Right;
                        break;


                }
                hDirectionTimer -= 3;
            }
            if (vDirectionTimer > 1.0)
            {
                switch (Veritcle)
                {
                    //    case Direction.Up:
                    //        Direction = Direction.Down;
                    //        break;
                    //    case Direction.Down:
                    //        Direction = Direction.Right;
                    //        break;
                    case Direction.Down:
                        Veritcle = Direction.Up;
                        break;
                    case Direction.Up:
                        Veritcle = Direction.Down;
                        break;


                }
                vDirectionTimer -= 1;
            }

            //Move the bat in the direction it is flying

            Debug.WriteLine((float)((int)Veritcle * -0.5));

            switch (Horizontal)
            {
                case Direction.Right:
                    Position += new Vector2(2, 1 + ((int)Veritcle * -1)) * 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Left:
                    Position += new Vector2(-2, 1 + ((int)Veritcle * -1)) * 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }
        }

        /// <summary>
        /// Draws the animated bat sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The SpriteBatch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            
            //Update animation frame
            if (animationTimer > 0.3)
            {
                animationFrame++;
                animationTimer -= .3;
                if(animationFrame > 3) animationFrame = 1;
            }

            //Draw the sprite
            var source = new Rectangle(animationFrame * 32, (int)Horizontal * 32, 32, 32);
            spriteBatch.Draw(texture, Position, source, Color.White);
        }
    }
}
