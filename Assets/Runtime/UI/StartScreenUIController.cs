using System.Collections;
using Runtime.Game;
using UnityEngine;

namespace Runtime.UI
{
    public class StartScreenUIController : MonoBehaviour
    {
        [SerializeField] private GameObject title;
        [SerializeField] private GameObject startText;
        [SerializeField] private GameObject howToPlay;
        [SerializeField] private AnimationCurve startTextCurve;
        [SerializeField] private float startTextDuration = 1f;
        [SerializeField] private float toStartTextScale = 1.5f;

        private void OnEnable() 
        {
            title.SetActive(true);
            startText.SetActive(true);
            howToPlay.SetActive(true);
            StartCoroutine(StartTextRoutine());
            GameManager.OnGameStart += OnGameStart;
        }

        private void OnDisable() 
        {
            title.SetActive(false);
            startText.SetActive(false);
            howToPlay.SetActive(false);
            GameManager.OnGameStart -= OnGameStart;
        }

        private void OnGameStart() => enabled = false;

        private IEnumerator StartTextRoutine()
        {
            var t = 0f;
            var forward = true;
            while (enabled)
            {
                t += forward ? Time.deltaTime : -Time.deltaTime;
                startText.transform.localScale 
                    = Vector3.Lerp(Vector3.one, Vector3.one * toStartTextScale, startTextCurve.Evaluate(t / startTextDuration));
                if (t >= startTextDuration) forward = false;
                if (t <= 0) forward = true;
                yield return null;
            }
        }
    }
}