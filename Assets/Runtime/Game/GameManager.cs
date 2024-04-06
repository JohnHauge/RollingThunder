using UnityEngine;
using System;
using Runtime.ScriptableObjects;
using UnityEngine.SceneManagement;

namespace Runtime.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public static event Action OnGameStart;
        public static event Action OnGameEnd;
        public bool GameStarted { get; private set; } = false;
        public float GameSpeed { get; private set; } = 0f;
        public float TravelSpeed => GameSpeed * Time.deltaTime;
        public float NormalizedSpeed => GameSpeed / settings.MaxSpeed;
        public float SpawnRate => Mathf.Lerp(settings.SpawnRate.From, settings.SpawnRate.From, NormalizedSpeed);
        public float DistanceTravelled { get; private set; } = 0f;
        [SerializeField] private GameSettings settings;

        private void Awake()
        {
            if (Instance != null) Debug.LogWarning("Multiple GameManager instances found.");
            Instance = this;
            enabled = false;
        }

        public void StartGame()
        {
            OnGameStart?.Invoke();
            GameStarted = true;
            GameSpeed = settings.StartSpeed;
            enabled = true;
        }

        private void Update() 
        {
            GameSpeed += settings.Acceleration * Time.deltaTime;
            if (GameSpeed > settings.MaxSpeed) GameSpeed = settings.MaxSpeed;
            DistanceTravelled += TravelSpeed;
        }

        public void GameOver()
        {
            OnGameEnd?.Invoke();
            enabled = false;
            GameSpeed = 0f;
            Debug.Log("Game Over");
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}