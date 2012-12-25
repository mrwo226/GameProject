using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaActionLibrary.Collisions
{
    /// <summary>
    /// This class defines a bounding circle for a sprite or game object to be used for collision checking.
    /// </summary>
    public class Circle
    {
        #region Fields

        int radius;
        Point center;

        #endregion

        #region Properties

        public int Radius
        {
            get { return radius; }
        }

        public Point Center
        {
            get { return center; }
        }

        #endregion

        #region Constructor

        public Circle(int radius, Vector2 vectorCenter)
        {
            this.radius = radius;
            center.X = (int)vectorCenter.X;
            center.Y = (int)vectorCenter.Y;
        }

        #endregion

    }
}
