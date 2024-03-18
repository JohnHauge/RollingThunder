using UnityEngine;

namespace Runtime
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Snowball snowball;

        private void Update()
        {
            transform.position = Vector3.up * (snowball.Scale + 1f);
        }
    }
}