using Runtime.ScriptableObjects;
using UnityEngine;

namespace Runtime.Snow
{
    public class Snowball : MonoBehaviour
    {
        [SerializeField] private GameSettings settings;
        [SerializeField] private SnowballState[] snowballStates;
        [SerializeField] private Transform player;
        [SerializeField] private InputListener inputListener;
        public float Speed { get; private set; } = 0f;
        public float Scale { get; private set; } = 1f;
        public SnowballState ActiveState { get; private set; }
        private Vector3[] _lanes;
        private int _currentLane;
        private bool _canLean;
        private bool _leanLeft;
        private bool _leanRight;

        private Vector3 GetLeanPosition(Vector3 leanDirection)
        {
            if (leanDirection != Vector3.right && leanDirection != Vector3.left)
                throw new System.ArgumentException("Lean direction must be either right or left.");
            var child = transform.GetChild(0);
            var scale = transform.localScale.y;
            return child.localPosition + Vector3.Slerp(Vector3.up, leanDirection, 0.5f) * (scale / 2f);
        }

        private void Start()
        {
            SetActiveState(snowballStates[0]);
        }

        private void Update()
        {
            SetPlayer();
            SetSpeed();
            SetHorizontalSpeed();
            transform.localScale = Vector3.one * Scale;
        }

        private void SetActiveState(SnowballState state)
        {
            ActiveState = state;
            _lanes = new Vector3[state.Lanes];
            var leftLean = GetLeanPosition(Vector3.left);
            var rightLean = GetLeanPosition(Vector3.right);
            for (int i = 0; i < state.Lanes; i++)
            {
                var t = ((float)i + 1) / (state.Lanes + 1);
                _lanes[i] = Vector3.Slerp(leftLean, rightLean, t);
            }
            if (_lanes.Length == 1 && state.IsLeanable) _canLean = true;
            _currentLane = state.StartLane;
            inputListener.OnAPress += OnAPress;
            inputListener.OnDPress += OnDPress;
            inputListener.OnARelease += OnARelease;
            inputListener.OnDRelease += OnDRelease;
        }

        private void SetSpeed()
        {
            Speed += Time.deltaTime * Scale;
            Speed = Mathf.Clamp(Speed, 0f, Scale * settings.SpeedModifier);
        }

        private void OnAPress()
        {
            if (_currentLane > 0) _currentLane--;
            if (_currentLane == 0 && _canLean) _leanLeft = true;
        }

        private void OnDPress()
        {
            if (_currentLane < _lanes.Length - 1) _currentLane++;
            if (_currentLane == _lanes.Length - 1 && _canLean) _leanRight = true;
        }

        private void OnARelease()
        {
            _leanLeft = false;
            _canLean = ActiveState.IsLeanable && _currentLane == 0;
        }
        private void OnDRelease()
        {
            _leanRight = false;
            _canLean = ActiveState.IsLeanable && _currentLane == _lanes.Length - 1;
        }

        private void SetPlayer()
        {
            var target = _lanes[_currentLane];
            if (_leanLeft) target = GetLeanPosition(Vector3.left);
            if (_leanRight) target = GetLeanPosition(Vector3.right);
            player.position = transform.position + target;
        }

        private void SetHorizontalSpeed()
        {
            var x = player.position.x;
            var leftLean = transform.position.x + GetLeanPosition(Vector3.left).x;
            var rightLean = transform.position.x +  GetLeanPosition(Vector3.right).x;
            var target = (Mathf.InverseLerp(leftLean, rightLean, x) - 0.5f) * 2f;
            transform.position += Vector3.right * target * ActiveState.MaxHorizontalSpeed * Time.deltaTime;
        }
    }
}
