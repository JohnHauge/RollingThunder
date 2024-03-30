using System;
using Runtime.Game;
using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace Runtime.Level
{
    [Serializable]
    public abstract class LaneObject : MonoBehaviour, ILaneObject
    {
        [SerializeField] protected float speed;
        public IObjectPool<GameObject> objectPool;
        public abstract Snowball Snowball { get; set; }
        public abstract void OnHit(Snowball snowball);
        protected bool move = true;
        
        public virtual void Update()
        {
            if(!Snowball || !move) return;
            var travelSpeed = (Snowball.Speed - speed) * Time.deltaTime;
            transform.position += Vector3.back * travelSpeed;
            if(transform.position.z < -10) ReturnToPool();
        }

        protected void ReturnToPool() => objectPool.Release(gameObject);
    }
}