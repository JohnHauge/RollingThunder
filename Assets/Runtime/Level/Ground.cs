using Runtime.Game;
using UnityEngine;
namespace Runtime.Level
{
    public class Ground : MonoBehaviour
    {
        private Renderer _renderer;
        private float _position;
        private float TravelSpeed => GameManager.Instance.TravelSpeed;
        private void Start()
            => _renderer = GetComponent<MeshRenderer>();

        private void Update() 
        {
            _position -= TravelSpeed;
            _renderer.material.SetFloat("_Position", _position);
        }
        
    }
}