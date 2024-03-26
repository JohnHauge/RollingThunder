using System.Collections.Generic;
using Runtime.Snow;
using UnityEngine;

namespace Level
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Snowball snowball;
        [SerializeField] private int lanes = 9;
        [SerializeField] private float laneWidth = 2f;
        [SerializeField] private GameObject snowPilePrefab; // TODO : Change this
        [SerializeField] private GameObject Fence; // TODO : Change this
        private readonly List<GameObject> laneObjects = new();
        private Vector3[] spawnPoints;
        private Vector3 SpawnPoint => spawnPoints[Random.Range(0, lanes)];
        private void Start()
        {
            spawnPoints = new Vector3[lanes];
            var lanePosition = new Vector3(-laneWidth * (lanes / 2), 0f, transform.position.z);
            for (int i = 0; i < lanes; i++)
            {
                spawnPoints[i] = lanePosition;
                lanePosition.x += laneWidth;
            }

            InvokeRepeating(nameof(Spawn), 0f, 2f); // TODO : Change this
            InvokeRepeating(nameof(SpawnFence), 0f, 2.5f); // TODO : Change this
        }

        private void Spawn()
        {
            var spawnPoint = SpawnPoint;
            var laneObject = Instantiate(snowPilePrefab, spawnPoint, Quaternion.identity);
            laneObjects.Add(laneObject);
        }

        private void SpawnFence()
        {
            var spawnPoint = SpawnPoint;
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


        private void OnDrawGizmosSelected()
        {
            var lanePosition = new Vector3(-laneWidth * (lanes / 2), 0f, transform.position.z);
            for (int i = 0; i < lanes; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(lanePosition, Vector3.one * 0.5f);
                lanePosition.x += laneWidth;
            }
        }
    }
}
