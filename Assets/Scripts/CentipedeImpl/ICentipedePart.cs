using System;

namespace CentipedeImpl
{
    public interface ICentipedePart
    {
        // Representation of each centipede part
        Point Point { get; }
        Point PreviousPoint { get; }

        event Action<ShotResult, Point> Shot;
    }
}