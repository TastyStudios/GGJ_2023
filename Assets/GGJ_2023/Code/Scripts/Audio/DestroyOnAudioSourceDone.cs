using System;
using UnityEngine;

namespace GGJ_2023.Audio {
    public class DestroyOnAudioSourceDone : MonoBehaviour {
        private AudioSource audioSource;

        private void Awake() {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start() {
            if (audioSource.loop) {
                enabled = false;
                return;
            }
            
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
