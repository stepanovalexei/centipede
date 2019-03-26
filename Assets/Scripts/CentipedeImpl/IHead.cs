using UnityEngine;

namespace CentipedeImpl
{
    public interface IHead : ICentipedePart
    {
        void MoveTowards(Direction direction);
        void SpawnAt(Point point);
        void Destroy();
    }
}