using UnityEngine;
namespace Runtime.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewGameSettings", menuName = "ScriptableObjects/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float speedModifier = 2f;
        [SerializeField] private float scaleSpeed = 0.1f;

        public float SpeedModifier => speedModifier;
        public float ScaleSpeed => scaleSpeed;
    }
}