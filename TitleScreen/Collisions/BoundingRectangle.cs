using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TitleScreen.Collisions
{
    /// <summary>
    /// A struct representing circular bounds
    /// </summary>
    public struct BoundingRectangle  : Bounding
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        /// <summary>
        /// Constructs a new BoundingRectangle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param> 
        public BoundingRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        /// <summary>
        /// Constructs a new BoundingRectangle
        /// </summary>
        /// <param name="position"></param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param> 
        public BoundingRectangle(Vector2 position, float width, float height)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Detects a collision between two Bounding items
        /// </summary>
        /// <param name="other">The other bounding items</param>
        /// <returns>true = collision, false = no collison</returns>
        public bool CollidesWith(Bounding other)
        {
            return CollisionHelper.Collides(this, other);
        }

        /*
        /// <summary>
        /// Detects a collision between two BoundingCircles
        /// </summary>
        /// <param name="other">The other bounding circle</param>
        /// <returns>true = collision, false = no collison</returns>
        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        /// <summary>
        /// Detects a collision between a BoundingRectangle and BoundingCircle
        /// </summary>
        /// <param name="other">The BoundingCircle</param>
        /// <returns>true = collision, false = no collison</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this, other);
        }
        */
    }
}
