using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using TitleScreen.Collisions;
using TitleScreen.Sprites;
using TitleScreen.Sprites.Items;

namespace TitleScreen.Content
{
    public class Bullet : Item
    {

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds
        {
            get
            {
                return (BoundingRectangle)bounds;
            }
            set
            {
                bounds = value;
            }
        }

        public bool Visible = false;
        public bool shoot = true;
        public Direction dir;
        public Bullet()
        {
            pixelWidth = 40;
            pixelHeight = 40;
            falling = false;
            Position = new Vector2(100, 100);
            this.bounds = new BoundingRectangle(Position, pixelWidth, pixelHeight);
        }

        public bool collides(BoundingRectangle item)
        {
            return CollisionHelper.Collides(item, Bounds);
        }


        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Bullet");
        }
        public void EndOfScreen()
        {
            if (Position.X - 15> ScreenValues.ScreenWidth)
            {
                Visible = false;
            }
            else if (Position.X - 15 + pixelWidth < 0)
            {
                Visible = false;
            }
        }
        public override void Update(GameTime gameTime)
        {
            EndOfScreen();

            if (Visible && shoot)
            {
                int Offset = (dir == Direction.Left) ? -1 : 1;
                Position += new Vector2((float)(600 * Offset * gameTime.ElapsedGameTime.TotalSeconds), 0); 
                //this.bounds = new BoundingRectangle(Position, pixelWidth, pixelHeight);
            }
            else if(Visible && !shoot)
            {
                Position = this.updateFallVector(gameTime, Position);
            }
            BoundingRectangle temp = (BoundingRectangle)bounds;
            temp.X = Position.X;
            temp.Y = Position.Y;
            Bounds = temp;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                var source = new Rectangle(0, 0, pixelWidth, pixelHeight);
                SpriteEffects spriteEffect = (dir == Direction.Left) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                spriteBatch.Draw(texture, Position, source, Color.White, rotation, new Vector2(0, 0), 1, spriteEffect, 0);
            }
        }
    }
}
