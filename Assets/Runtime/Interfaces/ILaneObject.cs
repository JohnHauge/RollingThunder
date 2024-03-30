using UnityEngine;
using Runtime.Game;

namespace Runtime.Interfaces
{
    public interface ILaneObject
    {
        void OnHit(Snowball snowball);
    }
}