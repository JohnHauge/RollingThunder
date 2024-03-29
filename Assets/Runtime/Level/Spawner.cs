using System.Collections.Generic;
using Runtime.Game;
using Runtime.Level;
using UnityEngine;

namespace Level
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Slope slope;
        [SerializeField] private Snowball snowball;
        [SerializeField] private LaneObject snowPilePrefab;
        [SerializeField] private LaneObject[] hazzardObjects;
        private readonly List<LaneObject> laneObjects = new();
        private Vector3 RandomSpawnPoint => slope.LaneStartPoints[Random.Range(0, slope.Lanes)];
        private LaneObject RandomHazzardObject => hazzardObjects[Random.Range(0, hazzardObjects.Length)];
        
        private void Start()
        {
            InvokeRepeating(nameof(SpawnSnow), 0f, 2f); // TODO : Change this
            InvokeRepeating(nameof(SpawnHazzard), 0f, 2.5f); // TODO : Change this
        }

        private void SpawnSnow()
        {
            var spawnPoint = RandomSpawnPoint;
            var laneObject = Instantiate(snowPilePrefab, spawnPoint, Quaternion.identity);
            laneObjects.Add(laneObject);
        }

        private void SpawnHazzard()
        {
            var spawnPoint = RandomSpawnPoint;
            var laneObject = Instantiate(RandomHazzardObject, spawnPoint, Quaternion.identity) as HazzardObject;
            laneObject.OnHazzardCollision += OnHazzardSnowballCollision;
            laneObjects.Add(laneObject);
        }

        private void OnHazzardSnowballCollision(HazzardObject hazzardObject)
        {
            Debug.Log("Hazzard Snowball Collision");
            laneObjects.Remove(hazzardObject);
            //TODO : Implement
        }

        private void Update()
        {
            var objects = new List<LaneObject>(laneObjects);
            foreach (var laneObject in objects) //TODO : Object pooling
            {
                laneObject.transform.position += Vector3.back * snowball.Speed * Time.deltaTime;
                if (laneObject.transform.position.z < -5f || laneObject.gameObject.activeSelf == false)
                {
                    laneObjects.Remove(laneObject);
                    Destroy(laneObject.gameObject);
                }
            }
        }
    }
}
