using System;
using UnityEngine;

namespace Centipede
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
        public static bool CanMoveFrom(this Direction self, Direction direction)
        {
            switch (self)
            {
                case Direction.Left:
                    return direction != Direction.Right;

                case Direction.Right:
                    return direction != Direction.Left;

                case Direction.Top:
                    return direction != Direction.Bottom;

                case Direction.Bottom:
                    return direction != Direction.Top;

                default:
                    throw new ArgumentOutOfRangeException("self", self, null);
            }
        }

        public static Vector3 Offset(this Direction self)
        {
            switch (self)
            {
                case Direction.Left:
                    return new Vector3(-1, 0);
                case Direction.Right:
                    return new Vector3(1, 0);
                case Direction.Top:
                    return new Vector3(0, 1);
                case Direction.Bottom:
                    return new Vector3(0, -1);
                default:
                    throw new ArgumentOutOfRangeException("self", self, null);
            }
        }
    }
}