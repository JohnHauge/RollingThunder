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
            _animator = GetComponentInChildren<Animator>();
        }


        private void Update()
        {
            _animator?.SetBool("IsLeaningLeft", snowball.IsLeaningLeft);
            _animator?.SetBool("IsLeaningRight", snowball.IsLeaningRight);
            transform.position = Vector3.MoveTowards(transform.position, 
                snowball.GetLanePosition(), speed * Time.deltaTime);
        }
    }
}