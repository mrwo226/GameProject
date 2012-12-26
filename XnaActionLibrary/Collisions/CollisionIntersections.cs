using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaActionLibrary.Collisions
{
    public enum CollisionType
    {
        Passable,
        Impassable,
        Damage,
    }

    public static class CollisionIntersections
    {
        public static bool CircleRectangleIntersection(Circle circle, Rectangle rectangle)
        {
            Vector2 circleDistance = Vector2.Zero;
            circleDistance.X = Math.Abs(circle.Center.X - rectangle.Center.X);
            circleDistance.Y = Math.Abs(circle.Center.Y - rectangle.Center.Y);

            if (circleDistance.X > (rectangle.Width / 2 + circle.Radius)) { return false; }
            if (circleDistance.Y > (rectangle.Height / 2 + circle.Radius)) { return false; }

            if (circleDistance.X <= (rectangle.Width / 2)) { return true; }
            if (circleDistance.Y <= (rectangle.Height / 2)) { return true; }

            double cornerDistance_sq = Math.Pow(circleDistance.X - rectangle.Width / 2, 2) + Math.Pow(circleDistance.Y - rectangle.Height / 2, 2);

            return (cornerDistance_sq <= (circle.Radius ^ 2));
        }  

    }
}