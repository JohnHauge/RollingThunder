using UnityEngine;

namespace Runtime.Game
{
    public class Slope : MonoBehaviour
    {
        [SerializeField] private int lanes = 9;
        [SerializeField] private float laneWidth = 2f;
        public Vector3[] LaneStartPoints { get; private set; }
        public int Lanes => lanes;
        public float LeftEdge => transform.position.x - laneWidth * (lanes / 2) - laneWidth / 2;
        public float RightEdge => transform.position.x + laneWidth * (lanes / 2) + laneWidth / 2;

        private void Awake()
        {
            LaneStartPoints = new Vector3[lanes];
            var lanePosition = new Vector3(-laneWidth * (lanes / 2), 0f, transform.position.z);
            for (int i = 0; i < lanes; i++)
            {
                LaneStartPoints[i] = lanePosition;
                lanePosition.x += laneWidth;
            }
        }

        private void OnDrawGizmosSelected()
        {
            var lanePosition = new Vector3(-laneWidth * (lanes / 2), 0f, transform.position.z);
            for (int i = 0; i < lanes; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(lanePosition, Vector3.one * 0.5f);
                lanePosition.x += laneWidth;
            }
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector3(LeftEdge, 0f, transform.position.z), Vector3.one * 0.5f);
            Gizmos.DrawCube(new Vector3(RightEdge, 0f, transform.position.z), Vector3.one * 0.5f);
        }
    }
}