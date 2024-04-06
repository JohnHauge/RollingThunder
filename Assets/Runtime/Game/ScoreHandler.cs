namespace Runtime.Game
{
    using System;
    using UnityEngine;

    public class ScoreHandler : MonoBehaviour
    {
        public static event Action<PointValueType> OnScore;
        public static event Action<int> OnScoreChange;
        public static bool IsHighScore { get; private set; } = false;
        public static int Score { get; private set; }
        
        private void Start()
        {
            GameManager.OnGameStart += StartScoreTracking;
            GameManager.OnGameEnd += StopScoreTracking;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStart -= StartScoreTracking;
            GameManager.OnGameEnd -= StopScoreTracking;
        }

        private void StartScoreTracking()
        {
            Score = 0;
            OnScoreChange?.Invoke(Score);
        }

        private void StopScoreTracking()
        {
            GameManager.OnGameStart -= StartScoreTracking;
            GameManager.OnGameEnd -= StopScoreTracking;
            var highScore = PlayerPrefs.GetInt(Constants.HighScoreKey, 0);
            IsHighScore = Score > highScore;
            if (IsHighScore) PlayerPrefs.SetInt(Constants.HighScoreKey, Score);
        }

        public static void AddScore(PointValueType value)
        {
            Score += Constants.PointValues[value];
            OnScore?.Invoke(value);
            OnScoreChange?.Invoke(Score);
        }
    }
}