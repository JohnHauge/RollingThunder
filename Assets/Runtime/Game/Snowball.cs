using System;
using Runtime.Interfaces;
using Runtime.ScriptableObjects;
using UnityEngine;

namespace Runtime.Game
{
    public class Snowball : MonoBehaviour, IMoveable
    {
        [SerializeField] private GameSettings settings;
        [SerializeField] private Slope slope;
        [SerializeField] private SnowballState[] snowballStates;
        public float Speed { get; private set; } = 0f;
        public float Scale => transform.localScale.x;
        private int _currentState;
        public SnowballState ActiveState => snowballStates[_currentState];
        private int _currentLane;
        private int _growthIncrements;
        public bool IsLeaningLeft { get; private set; }
        public bool IsLeaningRight { get; private set; }

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

        public Vector3 GetLanePosition()
        {
            var leftLean = GetLeanPosition(Vector3.left);
            var rightLean = GetLeanPosition(Vector3.right);
            if (IsLeaningLeft) return leftLean;
            if (IsLeaningRight) return rightLean;
            var t = ((float)_currentLane + 1) / (ActiveState.Lanes + 1);
            var point = Vector3.Lerp(leftLean, rightLean, t);
            var direction = (point - transform.position).normalized;
            return transform.position + direction * transform.localScale.x; ;
        }

        private void Start()
        {
            SetActiveState(0);
        }

        private void Update()
        {
            SetSpeed();
            UpdateMovement();
        }

        private void SetActiveState(int index)
        {
            _currentState = index;
            var state = snowballStates[index];
            _currentLane = state.StartLane;
        }

        private void SetSpeed()
        {
            Speed += Time.deltaTime * Scale;
            Speed = Mathf.Clamp(Speed, 0f, Scale * settings.SpeedModifier);
        }

        public void OnMovePressed(Vector3 direction)
        {
            if (direction == Vector3.left && _currentLane > 0) _currentLane--;
            else if (direction == Vector3.right && _currentLane < ActiveState.Lanes - 1) _currentLane++;
            else if (direction == Vector3.left && _currentLane == 0) IsLeaningLeft = true;
            else if (direction == Vector3.right && _currentLane == ActiveState.Lanes - 1) IsLeaningRight = true;
        }

        public void OnMoveReleased(Vector3 direction)
        {
            if (direction == Vector3.left) IsLeaningLeft = false;
            else if (direction == Vector3.right) IsLeaningRight = false;
        }

        public void OnSnowPileCollision()
        {
            _growthIncrements++;
            if (_growthIncrements <= ActiveState.GrowthIncrements) UpdateScale(1);
            else if (_currentState + 1 < snowballStates.Length) SetActiveState(_currentState + 1);
        }

        public void OnHazzardCollision()
        {
            if (_growthIncrements > 5) UpdateScale(-5);
            else Die();
        }

        private void Die()
        {
            Debug.Log("You died!");
            throw new NotImplementedException();
        }
        private void UpdateScale(int increments = 0)
        {
            _growthIncrements += increments;
            var t = (float)_growthIncrements / ActiveState.GrowthIncrements;
            var maxScale = ActiveState.MaxScale;
            var minScale = _currentState > 0 ? snowballStates[_currentState - 1].MaxScale : 1f;
            transform.localScale = Vector3.Lerp(Vector3.one * minScale, Vector3.one * maxScale, t);
        }
        private void UpdateMovement()
        {
            var x = ((float)_currentLane + 1) / (ActiveState.Lanes + 1); ;
            x = (x * 2f) - 1f;
            x = IsLeaningLeft ? -1f : x;
            x = IsLeaningRight ? 1f : x;
            var target = ActiveState.MaxHorizontalSpeed * Time.deltaTime * new Vector3(x, 0f, 0f);
            if (transform.position.x + target.x - transform.localScale.x < slope.LeftEdge && x < 0f) return;
            if (transform.position.x + target.x + transform.localScale.x > slope.RightEdge && x > 0f) return;
            transform.position += target;
        }
    }
}