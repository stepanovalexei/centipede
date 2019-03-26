using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameLogic;
using MapLogic;
using UnityEngine;

namespace CentipedeImpl
{
    public class Centipede : MonoBehaviour
    {
        private const float moveFrequency = .2f;

        public event Action<EndCondition> GameEnd;
        private IHead Head { get; set; }
        private List<ITailPart> Tail { get; set; }

        [SerializeField] private GameObject headPrefab;
        [SerializeField] private GameObject tailPrefab;
        [SerializeField] private Map map;

        private Direction currentDirection = Direction.Left;
        private const int inititalSize = 4;
        private Point startingPoint = new Point(5, 19);
        private bool isDead = false;

        private void Move(Direction newDirection)
        {
            // Centipede changes direction when it's about to collide with mushroom or border
            var movedNext = TryMoveNext(newDirection);
            if (!movedNext)
            {
                newDirection = Direction.Bottom;
            }

            Head.MoveTowards(newDirection);

            for (var i = 0; i < Tail.Count; i++)
            {
                // Each part of the tail moves towards the part in front of it
                var part = Tail[i];
                if (i == 0)
                {
                    part.MoveTowards(Head);
                    continue;
                }

                var nextPart = Tail[i - 1];
                part.MoveTowards(nextPart);
            }

            if (!movedNext)
                SwapDirection();
        }

        private void SwapDirection()
        {
            currentDirection = currentDirection.Opposite();
        }

        private bool TryMoveNext(Direction direction)
        {
            var nextPoint = Head.Point.Clone();
            nextPoint.Move(direction.Offset());

            if (ReachedHorizontalBorder(nextPoint))
            {
                return false;
            }

            if (ReachedVerticalBorder(nextPoint))
            {
                GameEnd?.Invoke(EndCondition.Lose);
                return false;
            }

            var cell = map.CellAt(nextPoint);
            if (cell.HasMushroom)
            {
                return false;
            }

            return true;
        }

        private bool ReachedHorizontalBorder(Point position)
        {
            var positionX = position.X;
            var mapWidth = map.Cells.GetLength(0);

            var reached = positionX < 0 || positionX > mapWidth - 1;
            return reached;
        }

        private bool ReachedVerticalBorder(Point position)
        {
            var positionY = position.Y;
            var mapHeight = map.Cells.GetLength(1);

            var reached = positionY < 0 || positionY > mapHeight - 1;
            return reached;
        }


        public void Spawn()
        {
            // Spawn every centipede part as prefab 
            Tail = new List<ITailPart>();

            for (var i = 0; i < inititalSize; i++)
            {
                if (i == 0)
                {
                    var headComponent = Instantiate(headPrefab, startingPoint.ToVector3(), Quaternion.identity,
                        transform);
                    Head = headComponent.GetComponent<Head>();

                    Head.SpawnAt(startingPoint);
                    Head.Shot += HandleShot;
                    continue;
                }

                var tailPosition = new Point(startingPoint.X + i, startingPoint.Y);

                var tailComponent = Instantiate(tailPrefab, tailPosition.ToVector3(), Quaternion.identity, transform);
                var tail = tailComponent.GetComponent<Tail>();
                tail.Shot += HandleShot;

                tail.SpawnAt(tailPosition);
                Tail.Add(tail);
            }

            StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            while (!isDead)
            {
                Move(currentDirection);
                yield return new WaitForSeconds(moveFrequency);
            }

            yield return null;
        }

        private void HandleShot(ShotResult result, Point position)
        {
            switch (result)
            {
                case ShotResult.Shrink:

                    LeaveMushroom(position);
                    DestroyLastTailPart();
                    break;
                case ShotResult.Decapitation:

                    if (Tail.Count == 0)
                    {
                        isDead = true;
                        GameEnd?.Invoke(EndCondition.Win);
                        return;
                    }

                    var tailPoint = Tail[0].Point;
                    DestroyNearestTailPart();
                    SpawnNewHead(position, tailPoint);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }

        private void DestroyLastTailPart()
        {
            var lastPart = Tail.Count - 1;
            Tail[lastPart].Destroy();
            Tail.RemoveAt(lastPart);
        }

        private void DestroyNearestTailPart()
        {
            Tail[0].Destroy();
            Tail.RemoveAt(0);
        }

        private void SpawnNewHead(Point position, Point tailPoint)
        {
            Head.Destroy();
            Head = null;

            var headComponent = Instantiate(headPrefab, tailPoint.ToVector3(), Quaternion.identity,
                transform);
            Head = headComponent.GetComponent<Head>();
            Head.SpawnAt(position);
            Head.Shot += HandleShot;
        }

        private void LeaveMushroom(Point position)
        {
            map.SpawnMushroomAt(position);
        }
    }
}