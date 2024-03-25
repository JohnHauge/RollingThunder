using UnityEngine;

namespace Runtime.Snow
{
    public class SnowballCollisionHandler : MonoBehaviour
    {
        private const string SnowPileTag = "SnowPile";
        private const string HazzardTag = "Hazzard";
        private Snowball _snowball;

        private void Start()
        {
            _snowball = GetComponent<Snowball>();
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collision");
            switch (other.gameObject.tag)
            {
                case SnowPileTag:
                    _snowball.OnSnowPileCollision();
                    other.gameObject.SetActive(false);
                    break;
                case HazzardTag:
                    Debug.Log("Hit Hazzard");
                    break;
            }
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log("Trigger");
            switch (other.gameObject.tag)
            {
                case SnowPileTag:
                    _snowball.OnSnowPileCollision();
                    other.gameObject.SetActive(false);
                    break;
                case HazzardTag:
                    Debug.Log("Hit Hazzard");
                    break;
            }
        }
    }
}