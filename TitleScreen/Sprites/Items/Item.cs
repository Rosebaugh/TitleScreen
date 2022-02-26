using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using TitleScreen.Collisions;

namespace TitleScreen.Sprites.Items
{
    public abstract class Item : Sprite
    {
        public SpriteEffects spriteEffect;
        protected BoundingCircle bounds;
        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        protected int floor = 400;

        public bool falling = true;

        Vector2 velocity;
        Vector2 acceleration;
        float accelerationTimer;

        protected int direction;
        public void Spawn()
        {
            Random rand = new Random();
            direction = rand.Next(0, 2);
            velocity = new Vector2(20 * (-1 * direction), -50);
            acceleration = new Vector2(0, -100);
            accelerationTimer = (float).25;
        }

        public Vector2 updateFallVector(GameTime gameTime, Vector2 position)
        {
            if (!falling)
            {
                return position;
            }

            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            accelerationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (accelerationTimer < 0)
            {
                acceleration = new Vector2(0, 100);
            }
            velocity += acceleration * t;
            position += velocity * t;

            if (position.Y >= floor && acceleration.Y >= 0)
            {
                acceleration = new Vector2(0, 0);
                falling = false;
                return new Vector2(position.X, floor);
            }

            return position;
        }
   }
}
