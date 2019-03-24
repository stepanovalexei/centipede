using UnityEngine;

namespace Centipede
{
    public class Head : IHead
    {
        public Vector3 Position { get; private set; }
        public Vector3 PreviousPosition { get; private set; }

        public Head(Vector3 position)
        {
            Position = position;
        }

        public void MoveTowards(Direction direction)
        {
            PreviousPosition = Position;
            Position.Move(direction);
        }
    }
}