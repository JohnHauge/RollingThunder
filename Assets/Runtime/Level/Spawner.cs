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
        private readonly Dictionary<GameObject, LaneObjectPool> objectPools = new();
        private Vector3 RandomSpawnPoint => slope.LaneStartPoints[Random.Range(0, slope.Lanes)];
        private GameObject RandomHazzardObject => hazzardObjects[Random.Range(0, hazzardObjects.Length)];
        
        
        private void Start()
        {
            objectPools.Add(snowPilePrefab, new LaneObjectPool(snowball, snowPilePrefab, 10));
            foreach (var hazzardObject in hazzardObjects)
                objectPools.Add(hazzardObject, new LaneObjectPool(snowball, hazzardObject, 10));
            
            InvokeRepeating(nameof(SpawnSnow), 0f, 2f); // TODO : Change this
            InvokeRepeating(nameof(SpawnHazzard), 0f, 2.5f); // TODO : Change this
        }

        private void SpawnSnow()
        {
            if(!objectPools.ContainsKey(snowPilePrefab)) return;
            var laneObject = objectPools[snowPilePrefab].Get().GetComponent<LaneObject>();
            var spawnPoint = RandomSpawnPoint;
            laneObject.transform.position = spawnPoint;
        }

        private void SpawnHazzard()
        {
            if(!objectPools.ContainsKey(RandomHazzardObject)) return;
            var laneObject = objectPools[RandomHazzardObject].Get().GetComponent<LaneObject>();
            var spawnPoint = RandomSpawnPoint;
            laneObject.transform.position = spawnPoint;
        }
    }
}
