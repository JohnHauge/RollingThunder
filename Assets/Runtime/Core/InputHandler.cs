using System.Collections.Generic;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Core
{
    [RequireComponent(typeof(IMoveable))]
    public class InputHandler : MonoBehaviour
    {
        private readonly List<IMoveable> _moveable = new();
        private InputListener _inputListener;

        private void Awake() 
        {
            _inputListener = gameObject.AddComponent<InputListener>();
            _moveable.AddRange(GetComponents<IMoveable>());
        }

        private void OnEnable()
        {
            _inputListener.OnAPress += OnAPress;
            _inputListener.OnDPress += OnDPress;
            _inputListener.OnARelease += OnARelease;
            _inputListener.OnDRelease += OnDRelease;
        }

        private void OnDisable()
        {
            _inputListener.OnAPress -= OnAPress;
            _inputListener.OnDPress -= OnDPress;
            _inputListener.OnARelease -= OnARelease;
            _inputListener.OnDRelease -= OnDRelease;
        }

        private void OnAPress() => _moveable.ForEach(movable => movable.OnMovePressed(Vector3.left));
        private void OnDPress() => _moveable.ForEach(movable => movable.OnMovePressed(Vector3.right));
        private void OnARelease() => _moveable.ForEach(movable => movable.OnMoveReleased(Vector3.left));
        private void OnDRelease() => _moveable.ForEach(movable => movable.OnMoveReleased(Vector3.right));
    }
}