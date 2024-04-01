
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Core
{
    [RequireComponent(typeof(IMoveable))]
    public class InputHandler : MonoBehaviour
    {
        private IMoveable _moveable;
        private InputListener _inputListener;

        private void Awake()
        {
            _inputListener = gameObject.AddComponent<InputListener>();
            _moveable = GetComponent<IMoveable>();
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

        private void OnAPress()
        {
            if (_moveable.CanMove) _moveable.OnMovePressed(Vector3.left);
        }
        
        private void OnDPress()
        {
            if (_moveable.CanMove) _moveable.OnMovePressed(Vector3.right);
        }

        private void OnARelease()
        {
            if (_moveable.CanMove) _moveable.OnMoveReleased(Vector3.left);
        }

        private void OnDRelease()
        {
            if (_moveable.CanMove) _moveable.OnMoveReleased(Vector3.right);
        }
    }
}