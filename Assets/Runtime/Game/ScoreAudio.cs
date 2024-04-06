using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Game
{
    [RequireComponent(typeof(AudioSource))]
    public class ScoreAudio : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> scoreSounds;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            ScoreHandler.OnScore += OnScore;
        }

        private void OnScore(PointValueType type)
        {
            if (scoreSounds.Count == 0) return;
            _audioSource.PlayOneShot(scoreSounds[UnityEngine.Random.Range(0, scoreSounds.Count)]);
        }
    }
}