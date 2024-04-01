namespace Runtime.Game
{
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(TextMeshPro))]
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private Snowball snowball;
        [SerializeField] private Vector3 startOffsetPostion;
        [SerializeField] private Vector3 endPosition;
        [SerializeField] private AnimationCurve animationCurve;
        private readonly float _animationTime = 1f;
        private TextMeshPro _text;
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private float _t;

        private void Start()
        {
            _text = GetComponent<TextMeshPro>();
            ScoreHandler.OnScore += Show;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            ScoreHandler.OnScore -= Show;
        }

        private void Show(PointValueType scoreType)
        {
            var score = Constants.PointValues[scoreType];
            _text.color = Constants.PointColors[scoreType];
            _t = 0f;
            _text.text = score.ToString();
            transform.position = snowball.GetLanePosition() + startOffsetPostion;
            _startPosition = transform.position;
            _endPosition = transform.position + endPosition;
            Debug.Log("Score: " + score);
            gameObject.SetActive(true);
        }

        private void Update()
        {
            _t += Time.deltaTime / _animationTime;
            var position = Vector3.Lerp(_startPosition, _endPosition, animationCurve.Evaluate(_t));
            transform.position = position;
            if (_t >= 1f)
            {
                gameObject.SetActive(false);
                Debug.Log("Score text hidden");
            }
        }
    }
}