using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaActionLibrary.SpriteClasses
{
    // The possible directions the character could face.
    public enum CardinalDirection
    {
        North,
        South,
        East,
        West,
        Northeast,
        Northwest,
        Southeast,
        Southwest,
    }

    public static class Direction
    {
        /// <summary>
        /// Determines the current direction the sprite is moving in cardinal directions.
        /// </summary>
        public static CardinalDirection determineCurrentDirection(Vector2 motion, CardinalDirection orientation)
        {
            if (motion.X > 0)
            {
                if (motion.Y > 0)
                    orientation = CardinalDirection.Southeast;
                else if (motion.Y < 0)
                    orientation = CardinalDirection.Northeast;
                else // y == 0
                    orientation = CardinalDirection.East;
            }
            else if (motion.X < 0)
            {
                if (motion.Y > 0)
                    orientation = CardinalDirection.Southwest;
                else if (motion.Y < 0)
                    orientation = CardinalDirection.Northwest;
                else // y == 0
                    orientation = CardinalDirection.West;
            }
            else // x == 0
            {
                if (motion.Y > 0)
                    orientation = CardinalDirection.South;
                else if (motion.Y < 0)
                    orientation = CardinalDirection.North;
            }

            return orientation;
        }
    }
}