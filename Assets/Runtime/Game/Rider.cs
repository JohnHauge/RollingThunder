using System.Collections;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Game
{
    public class Rider : MonoBehaviour
    {
        [SerializeField] private Snowball snowball;
        [SerializeField] private float speed;
        [SerializeField] private Vector3 offset;
        
        private Animator _animator;
        private float TravelSpeed => GameManager.Instance.GameSpeed;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            GameManager.OnGameStart += OnGameStart;
            GameManager.OnGameEnd += OnGameEnd;
            enabled = false;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStart -= OnGameStart;
            GameManager.OnGameEnd -= OnGameEnd;
        }

        private void OnGameStart()
        {
            enabled = true;
            _animator.SetBool("IsPlaying", true);
        }

        private void OnGameEnd()
        {
            enabled = false;
            StartCoroutine(OnFallRoutine());
        }

        private void Update()
        {
            _animator.SetFloat("Speed", TravelSpeed / 2f);
            _animator.SetBool("IsLeaningLeft", snowball.IsLeaningLeft);
            _animator.SetBool("IsLeaningRight", snowball.IsLeaningRight);
            transform.position = Vector3.MoveTowards(transform.position, 
                snowball.GetLanePosition() + offset, speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) return;
            if (!other.transform.TryGetComponent<ILaneObject>(out var laneObject)) return;
            GameManager.Instance.GameOver();
        }

        private IEnumerator OnFallRoutine()
        {
            _animator.SetBool("IsPlaying", false);
            var t = 0f;
            var fromPosition = transform.position;
            var targetPosition = transform.position;
            targetPosition.z += 2f;
            targetPosition.y = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(fromPosition, targetPosition, t);
                yield return null;
            }
            _animator.SetBool("InGround", true);
        }
    }
}