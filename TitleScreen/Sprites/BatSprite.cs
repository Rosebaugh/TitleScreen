using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace TitleScreen.Sprites
{

    /// <summary>
    /// A class representing a bat sprite
    /// </summary>
    public class BatSprite : Sprite
    {

        private double hDirectionTimer;
        private double vDirectionTimer;
        private double animationTimer;
        private short animationFrame;

        public short DirectionTime = 0;

        /// <summary>
        /// The direction of the bat
        /// </summary>
        public Direction Horizontal;
        public Direction Veritcle;

        public BatSprite()
        {
            pixelWidth = 32;
            pixelHeight = 32;
        }

        /// <summary>
        /// Loads the bat sprite texture
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("32x32-bat-sprite");
            if(ScreenValues.GameState.TitleScreen == ScreenValues.State)
            {
                DirectionTime = 3;
            }
            else if (DirectionTime == 0)
            {
                Random rnd = new Random();
                DirectionTime = (short)rnd.Next(2, 6);
            }
        }

        /// <summary>
        /// Updates the bat sprite to fly in a pattern
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            //Update the Direction Timer

            //Switch directions every 2 seconds
            vDirectionTimer += gameTime.ElapsedGameTime.TotalSeconds;
            hDirectionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (hDirectionTimer > DirectionTime)
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
                hDirectionTimer -= DirectionTime;
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
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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
            var source = new Rectangle(animationFrame * pixelWidth, (int)Horizontal * pixelHeight, pixelWidth, pixelHeight);
            spriteBatch.Draw(texture, Position, source, Color.White);
        }
    }
}
