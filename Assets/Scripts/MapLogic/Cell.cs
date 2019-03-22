using UnityEngine;

namespace MapLogic
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private GameObject mushroomPrefab;

        public bool HasMushroom { get; private set; }
        public Vector2 Position { get; private set; }

        public void SpawnMushroom()
        {
            var mushroom = Instantiate(mushroomPrefab, new Vector3(Position.x, Position.y, -0.1f), Quaternion.identity, transform);

            HasMushroom = true;
        }

        public void Set(Vector2 position)
        {
            Position = position;
        }
        
    }
}