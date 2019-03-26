using System;
using UnityEngine;
using DG.Tweening;
using GameLogic;

namespace CentipedeImpl
{
    public class Head : MonoBehaviour, IHead
    {
        public Point Point { get; set; }
        public Point PreviousPoint { get; private set; }
        public ShotResult Result { get; } = ShotResult.Decapitation;
        public event Action<ShotResult, Point> Shot;

        public void MoveTowards(Direction direction)
        {
            PreviousPoint = Point.Clone();
            Point.Move(direction.Offset());

            transform.position = new Vector3(Point.X, Point.Y);
        }

        public void SpawnAt(Point point)
        {
            Point = point;
        }

        public void Destroy()
        {
            Destroy(transform.gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            var bullet = other.collider.GetComponent<Bullet>();
            if (bullet)
            {
                Shot?.Invoke(Result, Point);
            }
        }
    }
}