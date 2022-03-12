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

namespace TitleScreen.Sprites.Items
{
    public class Gun2 : Item
    {
        public Bullet[] bullets;
        public int BulletCount;
        private SoundEffect shoot;
        public BoundingCircle Bounds 
        { 
            get{
                return (BoundingCircle)bounds;
            }
            set{
                bounds = value;
            }
        }
        public Gun2(Vector2 position)
        {
            Position = position;
            pixelWidth = 32;
            pixelHeight = 25;
            spriteEffect = SpriteEffects.None;

            this.bounds = new BoundingCircle(position + new Vector2((pixelWidth + 8) /2, (pixelWidth + 8) / 2), (pixelWidth + 8) / 2);
            Spawn();

            bullets = new Bullet[]
            {
                new Bullet(),
                new Bullet(),
                new Bullet(),
                new Bullet(),
                new Bullet(),
                new Bullet()
            };
        }
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Gun2");
            foreach (Bullet bullet in bullets) bullet.LoadContent(content);
            shoot = content.Load<SoundEffect>("Shoot");
        }

        public void Shoot()
        {
            if(BulletCount > 0)
            {
                foreach (Bullet bullet in bullets)
                {
                    if (!bullet.Visible)
                    {
                        bullet.Visible = true;
                        shoot.Play();
                        bullet.Position = (spriteEffect == SpriteEffects.FlipHorizontally) ? new Vector2(Position.X, Position.Y - 13) : new Vector2(Position.X + pixelWidth, Position.Y - 13);
                        bullet.dir = (spriteEffect == SpriteEffects.None) ? Direction.Left : Direction.Right;
                        BulletCount--;
                        return;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Bullet bullet in bullets) bullet.Update(gameTime);

            Position = this.updateFallVector(gameTime, Position);
            BoundingCircle temp = (BoundingCircle)bounds;
            temp.Center = Position + new Vector2((pixelWidth + 8) / 2, (pixelWidth + 8) / 2);
            Bounds = temp;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var source = new Rectangle(0, 0, pixelWidth, pixelHeight);
            foreach (Bullet bullet in bullets) bullet.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(texture, Position, source, Color.White, 0, new Vector2(0, 0), 1, spriteEffect, 0);
        }

    }
}
