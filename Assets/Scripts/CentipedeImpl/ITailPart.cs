using UnityEngine;

namespace CentipedeImpl
{
    public interface ITailPart : ICentipedePart
    {
        void MoveTowards(ICentipedePart part);
        void SpawnAt(Point point);
        void Destroy();
    }
}