using UnityEngine;

namespace Runtime
{
    public class SpeedDebug : MonoBehaviour
    {
        [SerializeField] private Snowball snowball;
        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            transform.position += Vector3.back * snowball.Speed * Time.deltaTime;
            if(transform.position.z < -5f)
                transform.position = startPosition;

        }
    }
}