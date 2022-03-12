using System;
using System.Collections.Generic;
using System.Text;

namespace TitleScreen.Collisions
{
    public interface Bounding
    {
         /// <summary>
         /// Detects a collision between two Bounding items
         /// </summary>
         /// <param name="other">The other bounding item</param>
         /// <returns>true = collision, false = no collison</returns>
        public bool CollidesWith(Bounding other)
        {
            return CollisionHelper.Collides(this, other);
        }
    }
}
