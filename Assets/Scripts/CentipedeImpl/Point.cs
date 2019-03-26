using UnityEngine;

namespace CentipedeImpl
{
    public class Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y);
        }

        public void Move(Point offset)
        {
            X += offset.X;
            Y += offset.Y;
        }

        public Point Clone()
        {
            return new Point(X, Y);
        }
    }
}