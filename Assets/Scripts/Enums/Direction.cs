using System;
using UnityEngine;

namespace CentipedeImpl
{
    public enum Direction
    {
        Left,
        Right,
        Top,
        Bottom
    }

    public static class DirectionExtensions
    {
        public static Direction Opposite(this Direction self)
        {
            switch (self)
            {
                case Direction.Left:
                    return Direction.Right;
                
                case Direction.Right:
                    return Direction.Left;
                
                case Direction.Top:
                    return Direction.Bottom;
                
                case Direction.Bottom:
                    return Direction.Top;

                default:
                    throw new ArgumentOutOfRangeException("self", self, null);
            }
        }

        public static Point Offset(this Direction self)
        {
            switch (self)
            {
                case Direction.Left:
                    return new Point(-1, 0);

                case Direction.Right:
                    return new Point(1, 0);

                case Direction.Top:
                    return new Point(0, 1);

                case Direction.Bottom:
                    return new Point(0, -1);

                default:
                    throw new ArgumentOutOfRangeException("self", self, null);
            }
        }
    }
}