using UnityEngine;
using Runtime.Snow;

namespace Runtime
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Snowball snowball;
        private Vector3 up;
        private Vector3 right;
        private Vector3 left;

        private void OnDrawGizmos() {
            var child = snowball.transform.GetChild(0);
            var scale = snowball.transform.localScale.y;
            up = child.position + Vector3.up * (scale / 2f);
            right = child.position + Vector3.Slerp(Vector3.up, Vector3.right, 0.5f) * (scale / 2f);
            left = child.position + Vector3.Slerp(Vector3.up, Vector3.left, 0.5f) * (scale / 2f);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(up, 0.1f);
           
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(right, 0.1f);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(left, 0.1f);
        }
    }
}