using System.Collections.Generic;

namespace Centipede
{
    public interface ICentipede
    {
        IHead Head { get; }
        List<ITailPart> Tail { get; }

        void Move(Direction? newDirection);
    }
}