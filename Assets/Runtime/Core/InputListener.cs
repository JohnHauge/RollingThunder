using UnityEngine;

namespace Runtime.Core
{
    public class InputListener : MonoBehaviour
    {
        public delegate void ButtonPressAction();
        public delegate void ButtonReleaseAction();

        public event ButtonPressAction OnAPress;
        public event ButtonPressAction OnDPress;
        public event ButtonReleaseAction OnARelease;
        public event ButtonReleaseAction OnDRelease;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) OnAPress?.Invoke();
            if (Input.GetKeyDown(KeyCode.D)) OnDPress?.Invoke();
            if (Input.GetKeyUp(KeyCode.A)) OnARelease?.Invoke();
            if (Input.GetKeyUp(KeyCode.D)) OnDRelease?.Invoke();
        }
    }
}