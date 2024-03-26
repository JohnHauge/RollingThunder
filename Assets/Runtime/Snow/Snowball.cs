using System;
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
        [SerializeField] private SphereCollider collider;
        public float Speed { get; private set; } = 0f;
        public float Scale => transform.localScale.x;
        private int _currentState;
        public SnowballState ActiveState => snowballStates[_currentState];
        private int _currentLane;
        private int _health;
        private int _growthIncrements;
        private bool _canLean;
        private bool _leanLeft;
        private bool _leanRight;
        

        private Vector3 GetLeanPosition(Vector3 leanDirection)
        {
            if (leanDirection != Vector3.right && leanDirection != Vector3.left)
                throw new ArgumentException("Lean direction must be either right or left.");
            var child = transform.GetChild(0);
            var scale = transform.localScale.y;
            var up = Vector3.up * scale / 2f;
            leanDirection = leanDirection * scale / 2f;
            return child.position + Vector3.Slerp(up, leanDirection, 0.5f);
        }

        private Vector3 GetLanePosition(int lane)
        {
            var leftLean = GetLeanPosition(Vector3.left);
            var rightLean = GetLeanPosition(Vector3.right);
            var t = ((float)lane + 1) / (ActiveState.Lanes + 1);
            var point = Vector3.Lerp(leftLean, rightLean, t);
            var direction = (point - transform.position).normalized;
            return transform.position + direction * transform.localScale.x;;
        }

        private float GetLanePoint(int lane){
            var t = ((float)lane + 1) / (ActiveState.Lanes + 1);
            return (t * 2f) - 1f;
        }

        private void Start()
        {
            SetActiveState(0);
            collider ??= GetComponent<SphereCollider>();
            _health = settings.Health;
        }

        private void Update()
        {
            SetPlayerPosition();
            SetSpeed();
            SetHorizontalMovement();
        }

        private void SetActiveState(int index)
        {
            _currentState = index;
            var state = snowballStates[index];
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
            if (_currentLane < ActiveState.Lanes - 1) _currentLane++;
            if (_currentLane == ActiveState.Lanes - 1 && _canLean) _leanRight = true;
        }

        private void OnARelease()
        {
            _leanLeft = false;
            _canLean = ActiveState.IsLeanable && _currentLane == 0;
        }
        private void OnDRelease()
        {
            _leanRight = false;
            _canLean = ActiveState.IsLeanable && _currentLane == ActiveState.Lanes - 1;
        }

        public void OnSnowPileCollision()
        {
            if (_health < settings.Health) _health++;
            else
            {
                _growthIncrements++;
                if (_growthIncrements <= ActiveState.GrowthIncrements)
                {
                    Debug.Log("Growth increment: " + _growthIncrements);
                    var t = (float)_growthIncrements / ActiveState.GrowthIncrements;
                    var maxScale = ActiveState.MaxScale;
                    var minScale =_currentState > 0 ? snowballStates[_currentState - 1].MaxScale : 1f;
                    transform.localScale = Vector3.Lerp(Vector3.one * minScale, Vector3.one * maxScale, t);
                }
            }
        }

        public void OnHazzardCollision()
        {
            if (_health > 0) _health--;
            else Die();
            Debug.Log("Health: " + _health);
        }

        private void Die()
        {
            Debug.Log("You died!");
            throw new NotImplementedException();
        }

        private void SetPlayerPosition()
        {
            var target = GetLanePosition(_currentLane);
            if (_leanLeft) target = GetLeanPosition(Vector3.left);
            if (_leanRight) target = GetLeanPosition(Vector3.right);
            player.position = target;
        }

        private void SetHorizontalMovement()
        {
            var x = GetLanePoint(_currentLane);
            x = _leanLeft ? -1f : x;
            x = _leanRight ? 1f : x;
            var target = new Vector3(x, 0f, 0f);
            transform.position += ActiveState.MaxHorizontalSpeed * Time.deltaTime * target;
        }

        private void OnDrawGizmos() {
            if (collider == null) return;
            Gizmos.color = Color.red;
            for (var i = 0; i < ActiveState.Lanes; i++)
            {
                var lanePosition = GetLanePosition(i);
                Gizmos.DrawSphere(lanePosition, 0.1f);
            }
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(GetLeanPosition(Vector3.left), 0.1f);
            Gizmos.DrawSphere(GetLeanPosition(Vector3.right), 0.1f);
        }
    }
}
