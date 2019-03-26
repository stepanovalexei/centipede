using System;
using System.Collections;
using CentipedeImpl;
using DG.Tweening;
using MapLogic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameLogic
{
    public class Game : MonoBehaviour
    {
        private const float mushroomsSpawnRate = 1f;

        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Map map;
        [SerializeField] private Centipede centipede;
        [SerializeField] private Text endText;

        private readonly Vector3 spawnPosition = new Vector3(0, 0, -.25f);
        private bool isGameOver = false;


        private void Start()
        {
            // Game-starting sequence
            map.Create();
            SpawnPlayer();
            SpawnCentipede();
            
            StartCoroutine(SpawnMushrooms());
        }

        private void SpawnPlayer()
        {
            Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }

        private void SpawnCentipede()
        {
            centipede.Spawn();
            centipede.GameEnd += EndSequence;
        }

        public void GameOver()
        {
            // Restart game 
            SceneManager.LoadScene(0);
        }

        private void EndSequence(EndCondition condition)
        {
            isGameOver = true;
            
            endText.transform.gameObject.SetActive(true);
            var color = new Color();
            switch (condition)
            {
                case EndCondition.Win:
                    endText.text = "YOU WIN";
                    color = Color.green;
                    break;
                case EndCondition.Lose:
                    endText.text = "YOU LOSE";
                    color = Color.red;
                    break;
            }

            endText.DOColor(color, 1f).onComplete += GameOver;
        }

        private IEnumerator SpawnMushrooms()
        {
            while (!isGameOver)
            {
                map.SpawnMushroom();
                yield return new WaitForSeconds(mushroomsSpawnRate);
            }
        }
    }
}