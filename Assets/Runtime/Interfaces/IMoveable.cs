using UnityEngine;

namespace Runtime.Interfaces
{
    public interface IMoveable
    {
        bool CanMove { get; }
        void OnMovePressed (Vector3 direction);
        void OnMoveReleased (Vector3 direction);
    }
}