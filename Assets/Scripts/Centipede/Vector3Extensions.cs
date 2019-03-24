using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Centipede
{
    public static class Vector3Extensions
    {
        public static Vector3 Move(this Vector3 self, Direction newDirection)
        {
            switch (newDirection)
            {
                case Direction.Left:
                    return new Vector3(self.x - 1, self.y);
                case Direction.Right:
                    return new Vector3(self.x + 1, self.y);
                case Direction.Top:
                    return new Vector3(self.x, self.y + 1);
                case Direction.Bottom:
                    return new Vector3(self.x, self.y - 1);
                default:
                    throw new ArgumentOutOfRangeException("newDirection", newDirection, null);
            }
        }
    }
}