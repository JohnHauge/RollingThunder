namespace Runtime.Level
{
    using Runtime.Game;
    using UnityEngine;
    
    public class SnowParticles : MonoBehaviour {
        [SerializeField] private ParticleSystem particles;

        private ParticleSystem.VelocityOverLifetimeModule _velocityOverLifetime;

        private void Start() {
            _velocityOverLifetime = particles.velocityOverLifetime;
        }

        private void Update() {
            _velocityOverLifetime.y = -GameManager.Instance.GameSpeed;
        }
    }
}