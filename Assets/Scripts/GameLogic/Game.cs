using UnityEngine;

namespace GameLogic
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;

        private Player player;

        private void Start()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            var spawnPosition = Vector3.zero;
            var spawnedPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            player = spawnedPlayer.GetComponent<Player>();
        }
    }
}