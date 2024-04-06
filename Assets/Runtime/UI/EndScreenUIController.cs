using Runtime.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class EndScreenUIController : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverText;
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private GameObject highScoreText;
        [SerializeField] private Button playAgainButton;

        private void Start()
        {
            GameManager.OnGameEnd += OnGameEnd;
            playAgainButton.onClick.AddListener(OnPlayAgain);
            enabled = false;
        }

        private void OnDestroy()
        {
            GameManager.OnGameEnd -= OnGameEnd;
            playAgainButton.onClick.RemoveListener(OnPlayAgain);
        }

        private void OnDisable()
        {
            gameOverText.SetActive(false);
            finalScoreText.gameObject.SetActive(false);
            highScoreText.SetActive(false);
            playAgainButton.gameObject.SetActive(false);
        }

        private void OnGameEnd()
        {
            finalScoreText.text = $"Final Score: {ScoreHandler.Score}";
            highScoreText.SetActive(ScoreHandler.IsHighScore);
            gameOverText.SetActive(true);
            finalScoreText.gameObject.SetActive(true);
            playAgainButton.gameObject.SetActive(true);
        }

        private void OnPlayAgain() => GameManager.Instance.RestartGame();
    }
}