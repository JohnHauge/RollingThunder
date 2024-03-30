using Runtime.Game;
using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace Runtime.Level
{
    public abstract class LaneObject : MonoBehaviour, ILaneObject
    {
        public IObjectPool<GameObject> objectPool;
        public abstract Snowball Snowball { get; set; }
        public abstract void OnHit(Snowball snowball);
        protected bool move = true;
        
        public virtual void Update()
        {
            if(!Snowball || !move) return;
            transform.position += Vector3.back * Snowball.Speed * Time.deltaTime;
            if(transform.position.z < -10) ReturnToPool();
        }

        protected void ReturnToPool() => objectPool.Release(gameObject);
    }
}