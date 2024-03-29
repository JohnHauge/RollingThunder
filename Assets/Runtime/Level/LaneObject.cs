using Runtime.Game;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Level
{
    public abstract class LaneObject : MonoBehaviour, ILaneObject
    {
        public Transform Transform => transform;
        public abstract void OnHit(Snowball snowball);
    }
}