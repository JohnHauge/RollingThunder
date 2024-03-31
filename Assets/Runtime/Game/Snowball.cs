using System;
using Runtime.Interfaces;
using Runtime.ScriptableObjects;
using UnityEngine;

namespace Runtime.Game
{
    public class Snowball : MonoBehaviour, IMoveable
    {
        private const float MaxYRotation = 25f;
        [SerializeField] private GameSettings settings;
        [SerializeField] private Slope slope;
        [SerializeField] private SnowballState[] snowballStates;
        [SerializeField] private GameObject virtualCamera;
        public SnowballState[] SnowballStates => snowballStates;
        public float Angle { get; private set; } = 0f;
        private int _currentState;
        public SnowballState ActiveState => snowballStates[_currentState];
        private int _currentLane;
        public bool IsLeaningLeft { get; private set; }
        public bool IsLeaningRight { get; private set; }
        private Renderer _renderer;
        public SnowballScaleHandler Scale { get; private set; }
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

        public int GetClosestLaneIndex(Vector3 position)
        {
            var leftLean = GetLeanPosition(Vector3.left);
            var rightLean = GetLeanPosition(Vector3.right);
            var closestLaneIndex = 0;
            var closestDistance = float.MaxValue;

            for (int i = 0; i < ActiveState.Lanes; i++)
            {
                var t = ((float)i + 1) / (ActiveState.Lanes + 1);
                var point = Vector3.Lerp(leftLean, rightLean, t);
                var direction = (point - transform.position).normalized;
                var lanePosition = transform.position + direction * transform.localScale.x;
                var distance = Vector3.Distance(position, lanePosition);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestLaneIndex = i;
                }
            }
            return closestLaneIndex;
        }

        public Vector3 GetClosestLanePosition(Vector3 position)
        {
            var leftLean = GetLeanPosition(Vector3.left);
            var rightLean = GetLeanPosition(Vector3.right);
            var closestLaneIndex = GetClosestLaneIndex(position);
            var t = ((float)closestLaneIndex + 1) / (ActiveState.Lanes + 1);
            var point = Vector3.Lerp(leftLean, rightLean, t);
            var direction = (point - transform.position).normalized;
            return transform.position + direction * transform.localScale.x;
        }

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
            Scale = new SnowballScaleHandler(this);
            GameManager.OnGameStart += StartSnowball;
            enabled = false;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStart -= StartSnowball;
        }

        private void StartSnowball()
        {
            SetActiveState(0);
            virtualCamera.SetActive(true);
            enabled = true;
        }

        public void StopSnowBall()
        {

        }

        private void Update()
        {
            var speed = GameManager.Instance.GameSpeed;
            UpdateMovement();
            Angle += speed * Time.deltaTime * 25f;
            transform.GetChild(0).Rotate(Vector3.right, speed * Time.deltaTime * 30f);
        }

        private void SetActiveState(int index)
        {
            if (index >= snowballStates.Length) return;
            var lanePosition = GetLanePosition();
            _currentState = index;
            var state = snowballStates[index];
            _currentLane = GetClosestLaneIndex(lanePosition);
            _renderer.material.SetInt("_Lanes", state.Lanes);
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

        public void OnSnowPileCollision() => Scale.Grow();
        public void OnHazzardCollision(int damage) => Scale.Shrink(damage);
        public void CheckState(int increments)
        {
            var state = _currentState;
            if (increments < 0) state--;
            else if (increments > SnowballStates[_currentState].GrowthIncrements) state++;
            if (state < 0) Die();
            else if (state != _currentState) SetActiveState(state);
        }

        private void UpdateMovement()
        {
            var x = ((float)_currentLane + 1) / (ActiveState.Lanes + 1); ;
            x = (x * 2f) - 1f;
            x = IsLeaningLeft ? -1f : x;
            x = IsLeaningRight ? 1f : x;
            transform.eulerAngles = new Vector3(0f, MaxYRotation * x, 0f);
            var target = ActiveState.MaxHorizontalSpeed * Time.deltaTime * new Vector3(x, 0f, 0f);
            if (transform.position.x + target.x < slope.LeftEdge && x < 0f) return;
            if (transform.position.x + target.x > slope.RightEdge && x > 0f) return;
            transform.position += target;
        }

        private void Die()
        {
            Debug.Log("You died!");
            throw new NotImplementedException();
        }

        private void OnMouseDown() {
            GameManager.Instance.StartGame();
        }
    }
}