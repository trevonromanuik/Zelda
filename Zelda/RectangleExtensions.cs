using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zelda
{
    public static class RectangleExtensions
    {
        public static Vector2 GetIntersectionDepth(this Rectangle rectA, Rectangle rectB)
        {
            // Calculate half sizes.
            float halfWidthA = rectA.Width / 2;
            float halfHeightA = rectA.Height / 2;
            float halfWidthB = rectB.Width / 2;
            float halfHeightB = rectB.Height / 2;

            // Calculate centers
            Vector2 centerA = new Vector2(rectA.X + halfWidthA, rectA.Y + halfHeightA);
            Vector2 centerB = new Vector2(rectB.X + halfWidthB, rectB.Y + halfHeightB);

            // Calculate current and minimum-non-intersecting distances
            Vector2 distance = centerA - centerB;
            float minDistanceX = halfWidthA + halfWidthB;
            float minDistanceY = halfHeightA + halfHeightB;

            // If we are not intersecting, return (0, 0)
            if (Math.Abs(distance.X) >= minDistanceX || Math.Abs(distance.Y) >= minDistanceY)
                return Vector2.Zero;

            // Calculate and return intersection depths
            float depthX = distance.X > 0 ? minDistanceX - distance.X : -minDistanceX - distance.X;
            float depthY = distance.Y > 0 ? minDistanceY - distance.Y : -minDistanceY - distance.Y;
            return new Vector2(depthX, depthY);
        }
    }
}
