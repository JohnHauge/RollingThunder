using UnityEngine;

namespace Runtime.Snow
{
    [CreateAssetMenu(fileName = "NewSnowballState", menuName = "ScriptableObjects/SnowballState")]
    public class SnowballState : ScriptableObject
    {
        [SerializeField] private int lanes;
        [SerializeField] private int startLane;
        [SerializeField] private int growthIncrements;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float maxScale;
        [SerializeField] private bool isLeanable;

        public int Lanes => lanes;
        public int StartLane => startLane;
        public int GrowthIncrements => growthIncrements;
        public float MaxSpeed => maxSpeed;
        public float MaxScale => maxScale;
        public bool IsLeanable => isLeanable;
    }
}