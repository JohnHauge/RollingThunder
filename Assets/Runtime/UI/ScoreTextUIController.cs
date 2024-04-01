using System;
using Runtime.Game;
using UnityEngine;

namespace Runtime.UI
{
    public class ScoreTextUIController : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI scoreText;

        private void Start()
        {
            GameManager.OnGameStart += Show;
            GameManager.OnGameEnd += Hide;
            scoreText.text = "";
        }

        private void OnDestroy()
        {
            GameManager.OnGameStart -= Show;
            GameManager.OnGameEnd -= Hide;
        }

        private void Show()
        {
            scoreText.text = "0000p";
            ScoreHandler.OnScoreChange += UpdateScore;
        }

        private void Hide()
        {
            scoreText.text = "";
            ScoreHandler.OnScoreChange -= UpdateScore;
        }

        private void UpdateScore(int score)
        {
            scoreText.text = score.ToString("0000") + "p";
        }


    }
}