using UnityEngine;

namespace Runtime.Game
{
    public class Rider : MonoBehaviour
    {
        [SerializeField] private Snowball snowball;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }


        private void Update()
        {
            _animator?.SetBool("IsLeaningLeft", snowball.IsLeaningLeft);
            _animator?.SetBool("IsLeaningRight", snowball.IsLeaningRight);
            transform.position = snowball.GetLanePosition();
        }
    }
}