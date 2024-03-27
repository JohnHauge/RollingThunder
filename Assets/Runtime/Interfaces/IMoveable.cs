using UnityEngine;

namespace Runtime.Interfaces
{
    public interface IMoveable
    {
        void OnMovePressed (Vector3 direction);
        void OnMoveReleased (Vector3 direction);
    }
}