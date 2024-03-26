using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Snow
{
    [RequireComponent(typeof(Snowball))]
    public class SnowballCollisionHandler : MonoBehaviour
    {
        private const string SnowPileTag = "SnowPile";
        private const string HazzardTag = "Hazzard";
        private Snowball _snowball;
        private void Start() => _snowball = GetComponent<Snowball>();
        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.TryGetComponent<ILaneObject>(out var laneObject)) return;
            laneObject.OnSlopeLaneHit(_snowball);
        }
    }
}