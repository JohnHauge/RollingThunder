using UnityEngine;

namespace Runtime
{
    public class Snowball : MonoBehaviour
    {
        public float Speed { get; private set; } = 0f;
        public float Scale { get; private set;} = 1f;

        private void Update()
        {
            SetSpeed();
            SetScale();
            transform.localScale = Vector3.one * Scale;
        }

        private void SetSpeed()
        {
            Speed += Time.deltaTime * Scale;
            Speed = Mathf.Clamp(Speed, 0, Scale * 10f);
        }

        private void SetScale()
        {
            Scale += Time.deltaTime * Speed * 0.005f;
        }
    }
}
