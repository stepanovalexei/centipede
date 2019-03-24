using UnityEngine;

namespace Centipede
{
    public interface ITailPart : ICentipedePart
    {
        void MoveTowards(Direction direction);
    }
}