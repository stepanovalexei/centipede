using MapLogic;
using UnityEngine;

namespace GameLogic
{
    public class Game : MonoBehaviour
    {
        private const float mushroomsSpawnRate = 5f;
        
        [SerializeField] private GameObject playerPrefab;

        [SerializeField] private Map map; 

        private Player player;

        private void Start()
        {
            SpawnPlayer();
            map.Create();
        }

        private void SpawnPlayer()
        {
            var spawnPosition = Vector3.zero;
            var spawnedPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            player = spawnedPlayer.GetComponent<Player>();
        }
    }
}