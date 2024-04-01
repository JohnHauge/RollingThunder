using Runtime.Game;
using UnityEngine;
namespace Runtime.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewGameSettings", menuName = "ScriptableObjects/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float acceleration = 0.1f;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float startSpeed;
        [SerializeField] private FromTo spawnRate;
        public float Acceleration => acceleration;
        public float MaxSpeed => maxSpeed;
        public float StartSpeed => startSpeed;
        public FromTo SpawnRate => spawnRate;
    }
}