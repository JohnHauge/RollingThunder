using UnityEngine;
using Runtime.Game;

namespace Runtime.Interfaces
{
    public interface ILaneObject
    {
        public Transform transform { get; }
        void OnHit(Snowball snowball);
    }
}