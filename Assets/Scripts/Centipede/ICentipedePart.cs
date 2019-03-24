using UnityEngine;
using UnityEngine.XR.WSA.Persistence;

namespace Centipede
{
    public interface ICentipedePart
    {
        Vector3 Position { get; }
        Vector3 PreviousPosition { get; }
    }
}