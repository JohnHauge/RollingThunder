using System.Collections.Generic;
using Runtime.Core;
using Runtime.Game;
using Runtime.Level;
using UnityEngine;

namespace Level
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Slope slope;
        [SerializeField] private Snowball snowball;
        [SerializeField] private GameObject snowPilePrefab;
        [SerializeField] private GameObject[] hazzardObjects;
        [SerializeField] private int startObjectCount = 10;
        [SerializeField] private float startSpawnMargin = 1f;
        private readonly Dictionary<GameObject, LaneObjectPool> objectPools = new();
        private Vector3 RandomSpawnPoint => slope.LaneStartPoints[Random.Range(0, slope.Lanes)];
        private GameObject RandomHazzardObject => hazzardObjects[Random.Range(0, hazzardObjects.Length)];
        private float _targetTime;
        private float _time;

        
        private void Start()
        {
            objectPools.Add(snowPilePrefab, new LaneObjectPool(snowball, snowPilePrefab, 10));
            foreach (var hazzardObject in hazzardObjects)
                objectPools.Add(hazzardObject, new LaneObjectPool(snowball, hazzardObject, 10));
            _targetTime = GameManager.Instance.SpawnRate;
            enabled = false;
            StartSpawn();
            GameManager.OnGameStart += GameStart;
            GameManager.OnGameEnd += OnGameEnd;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStart -= GameStart;
            GameManager.OnGameEnd -= OnGameEnd;
        }

        private void StartSpawn()
        {
            for (int i = 0; i < startObjectCount; i++)
            {
                var randomObject = Random.Range(0, 2) == 0 ? SpawnSnow() : SpawnHazzard();
                if(randomObject == null) continue;
                var spawnPoint = RandomSpawnPoint;
                spawnPoint.z = Random.Range(0f + startSpawnMargin, slope.transform.position.z);
                randomObject.transform.position = spawnPoint;
            }
        }

        private void GameStart() => enabled = true;
        private void OnGameEnd() => enabled = false;

        private void Update()
        {
            _time += Time.deltaTime;
            if(_time < _targetTime) return;
            _time = 0f;
            _targetTime = GameManager.Instance.SpawnRate;
            var randomObject = Random.Range(0, 2) == 0 ? SpawnSnow() : SpawnHazzard();
            if(randomObject != null) randomObject.transform.position = RandomSpawnPoint;
        }

        private LaneObject SpawnSnow()
        {
            if(!objectPools.ContainsKey(snowPilePrefab)) return null;
            return objectPools[snowPilePrefab].Get().GetComponent<LaneObject>();
        }

        private LaneObject SpawnHazzard()
        {
            if(!objectPools.ContainsKey(RandomHazzardObject)) return null;
            return objectPools[RandomHazzardObject].Get().GetComponent<LaneObject>();
        }
    }
}
