using System;
using DG.Tweening;
using GameLogic;
using UnityEngine;

namespace CentipedeImpl
{
    public class Tail : MonoBehaviour, ITailPart
    {
        public Point Point { get; set; }
        public Point PreviousPoint { get; private set; }
        public event Action<ShotResult, Point> Shot;
        
        private ShotResult Result { get; } = ShotResult.Shrink;

        public void MoveTowards(ICentipedePart part)
        {
            PreviousPoint = Point.Clone();
            Point = (part.PreviousPoint);

            transform.position = new Vector3(Point.X, Point.Y);
        }

        public void SpawnAt(Point point)
        {
            Point = point;
        }

        private void OnCollisionEnter(Collision other)
        {
            var bullet = other.collider.GetComponent<Bullet>();
            if (bullet)
            {
                Shot?.Invoke(Result, Point);
            }
        }

        public void Destroy()
        {
            Destroy(transform.gameObject);
        }
    }
}