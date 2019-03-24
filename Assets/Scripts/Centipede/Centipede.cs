using System.Collections.Generic;

namespace Centipede
{
    public class Centipede : ICentipede
    {
        public IHead Head { get; private set; }
        public List<ITailPart> Tail { get; private set; }

        public Centipede(IHead head)
        {
            Head = head;
            Tail = new List<ITailPart>();
        }

        private Direction currentDirection = Direction.Right;
        
        public void Move(Direction? newDirection)
        {
            var useNewDirection = newDirection.HasValue && newDirection.Value.CanMoveFrom(currentDirection);

            var moveDirection = useNewDirection
                ? newDirection.Value
                : currentDirection;
        }
    }
}