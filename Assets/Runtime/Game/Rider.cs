using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Game
{
    public class Rider : MonoBehaviour
    {
        [SerializeField] private Snowball snowball;
        [SerializeField] private float speed;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetFloat("Speed", snowball.Speed / 2f);
            _animator.SetBool("IsLeaningLeft", snowball.IsLeaningLeft);
            _animator.SetBool("IsLeaningRight", snowball.IsLeaningRight);
            transform.position = Vector3.MoveTowards(transform.position, 
                snowball.GetLanePosition(), speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) return;
            if (!other.transform.TryGetComponent<ILaneObject>(out var laneObject)) return;
        }
    }
}