using Runtime.Game;
using Runtime.Level;
using UnityEngine;
using UnityEngine.Pool;

namespace Runtime.Core
{
    public class LaneObjectPool
    {
        private readonly IObjectPool<GameObject> _objectPool;
        private readonly GameObject _prefab;
        private readonly GameObject _spawnParent;
        private readonly Snowball _snowball;

        public LaneObjectPool(Snowball snowball, GameObject prefab, int initialSize)
        {
            _spawnParent = new GameObject(prefab.name + " Pool");
            _snowball = snowball;
            _prefab = prefab;
            _objectPool = new ObjectPool<GameObject>(OnCreate, OnGet, OnReturn, OnDestroy, true, initialSize);
        }

        private void OnGet(GameObject obj)
        {
            obj.transform.SetParent(_spawnParent.transform);
            obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            obj.SetActive(true);
        }

        private GameObject OnCreate()
        {
            var obj = Object.Instantiate(_prefab);
            var laneObject = obj.GetComponent<LaneObject>();
            laneObject.objectPool = _objectPool;
            laneObject.Snowball = _snowball;
            obj.transform.SetParent(_spawnParent.transform);
            return obj;
        }

        public void OnReturn(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(_spawnParent.transform);
        }

        private void OnDestroy(GameObject obj)
            => Object.Destroy(obj);

        public GameObject Get()
            => _objectPool.Get();

        public void Return(GameObject obj)
            => _objectPool.Release(obj);

    }
}