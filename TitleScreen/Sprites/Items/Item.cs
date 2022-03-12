using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using TitleScreen.Collisions;
using TitleScreen.Content;

namespace TitleScreen.Sprites.Items
{
    public abstract class Item : Sprite
    {
        public SpriteEffects spriteEffect;
        protected Bounding bounds;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public Bounding Bounds => bounds;

        public int ItemFloor = ScreenValues.ScreenHeight - 50;

        public bool falling = true;

        Vector2 velocity;
        Vector2 acceleration;
        float accelerationTimer;
        protected float rotation = 0;
        protected short rotationdir = 0;
        protected float rotationspeed = 0;

        protected int direction;
        public void Spawn()
        {
            Random rand = new Random();
            direction = rand.Next(0, 2) * 2 - 1;
            velocity = new Vector2(rand.Next(5, 25) * direction, -10 * rand.Next(4, 16));
            acceleration = new Vector2(0, -100);
            accelerationTimer = (float).25;
            rotationdir = (short)(rand.Next(0, 2) * 2 - 1);
            rotationspeed = (float)rand.NextDouble() * 2;

        }
        public bool collides(Bounding item)
        {
            if (this is Bullet b)
            {
                return b.Bounds.CollidesWith(item);
            }
            else if (this is Gun2 g)
            {
                return g.Bounds.CollidesWith(item);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public Vector2 updateFallVector(GameTime gameTime, Vector2 position)
        {
            if (!falling)
            {
                return position;
            }

            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            accelerationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(rotation > MathHelper.TwoPi)
            {
                rotation -= MathHelper.TwoPi;
            }
            else if (rotation < 0)
            {
                rotation += MathHelper.TwoPi;
            }

            rotation += MathHelper.TwoPi * (float)gameTime.ElapsedGameTime.TotalSeconds * rotationdir * rotationspeed;

            if (accelerationTimer < 0)
            {
                acceleration = new Vector2(0, 100);
            }
            velocity += acceleration * t;
            position += velocity * t;


            if (position.X < 30)
            {
                position.X = 30;
            }
            if (position.Y >= ItemFloor && acceleration.Y >= 0)
            {
                acceleration = new Vector2(0, 0);
                falling = false;
                return new Vector2(position.X, ItemFloor);
            }

            return position;
        }
   }
}
