using UnityEngine;
using System;
namespace Runtime.Game
{
    [CreateAssetMenu(fileName = "NewSnowballState", menuName = "ScriptableObjects/SnowballState")]
    public class SnowballState : ScriptableObject
    {
        [SerializeField] private int lanes;
        [SerializeField] private int startLane;
        [SerializeField] private int growthIncrements;
        [SerializeField] private float maxHorizontalSpeed;
        [SerializeField] private FromTo speed;
        [SerializeField] private FromTo scale;

        public int Lanes => lanes;
        public int StartLane => startLane;
        public int GrowthIncrements => growthIncrements;
        public float MaxHorizontalSpeed => maxHorizontalSpeed;
        public FromTo Speed => speed;
        public FromTo Scale => scale;
    }

    [Serializable]
    public struct FromTo
    {
        public float From;
        public float To;
    }
}