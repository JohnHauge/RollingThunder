namespace Runtime.Game
{
    using System;
    using UnityEngine;

    public class ScoreHandler : MonoBehaviour
    {
        public static event Action<PointValueType> OnScore;
        public static event Action<int> OnScoreChange;
        public static event Action<int> OnFinalScore;
        public static event Action<int> OnHighScore;

        private static int _score;

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
            _score = 0;
        }

        private void StopScoreTracking()
        {
            PlayerPrefs.SetInt(Constants.HighScoreKey, Mathf.Max(PlayerPrefs.GetInt(Constants.HighScoreKey), _score));
            OnFinalScore?.Invoke(_score);
        }

        public static void AddScore(PointValueType value)
        {
            _score += Constants.PointValues[value];
            OnScore?.Invoke(value);
            OnScoreChange?.Invoke(_score);
        }
    }
}