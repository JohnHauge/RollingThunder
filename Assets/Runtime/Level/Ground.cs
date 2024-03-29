using Runtime.Game;
using UnityEngine;
namespace Runtime.Level
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private Snowball _snowball;
        private Renderer _renderer;
        private float _position;
        private void Start()
            => _renderer = GetComponent<MeshRenderer>();

        private void Update() 
        {
            _position -= _snowball.Speed * Time.deltaTime;
            _renderer.material.SetFloat("_Position", _position);
        }
        
    }
}