using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Game
{
    [RequireComponent(typeof(Snowball))]
    public class SnowballCollisionHandler : MonoBehaviour
    {
        private Snowball _snowball;
        private void Start() => _snowball = GetComponent<Snowball>();
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player")) return;
            if (!other.transform.TryGetComponent<ILaneObject>(out var laneObject)) return;
            laneObject.OnHit(_snowball);
        }
    }
}