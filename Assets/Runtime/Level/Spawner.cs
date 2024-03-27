using System.Collections.Generic;
using Runtime.Game;
using UnityEngine;

namespace Level
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Slope slope;
        [SerializeField] private Snowball snowball;
        [SerializeField] private GameObject snowPilePrefab; // TODO : Change this
        [SerializeField] private GameObject Fence; // TODO : Change this
        private readonly List<GameObject> laneObjects = new();
        private Vector3 RandomSpawnPoint => slope.LaneStartPoints[Random.Range(0, slope.Lanes)];
        
        private void Start()
        {
            InvokeRepeating(nameof(Spawn), 0f, 2f); // TODO : Change this
            InvokeRepeating(nameof(SpawnFence), 0f, 2.5f); // TODO : Change this
        }

        private void Spawn()
        {
            var spawnPoint = RandomSpawnPoint;
            var laneObject = Instantiate(snowPilePrefab, spawnPoint, Quaternion.identity);
            laneObjects.Add(laneObject);
        }

        private void SpawnFence()
        {
            var spawnPoint = RandomSpawnPoint;
            var laneObject = Instantiate(Fence, spawnPoint, Quaternion.identity);
            laneObjects.Add(laneObject);
        }

        private void Update()
        {
            var objects = new List<GameObject>(laneObjects);
            foreach (var laneObject in objects) //TODO : Object pooling
            {
                if (laneObject.activeSelf == false)
                {
                    laneObjects.Remove(laneObject);
                    Destroy(laneObject);
                    continue;
                }
                laneObject.transform.position += Vector3.back * snowball.Speed * Time.deltaTime;
                if (laneObject.transform.position.z < -5f)
                {
                    laneObjects.Remove(laneObject);
                    Destroy(laneObject);
                }
            }
        }
    }
}
