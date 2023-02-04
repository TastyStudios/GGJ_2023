using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGJ_2023.Audio {
    public class AudioManager : MonoBehaviour {
        public static AudioManager Instance;

        [SerializeField] private List<TaggedMusicClip> musicClipList;
        [SerializeField] private List<TaggedSfxClip> sfxClipList;

        [SerializeField] private GameObject musicAudioSourcePrefab;
        [SerializeField] private GameObject sfxAudioSourcePrefab;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                Debug.LogError("There are more than one AudioManagers in this scene.");
                return;
            }

            Instance = this;
        }

        public void PlaySfx(Sfx sfx) {
            TaggedSfxClip taggedSfxClip = sfxClipList.First(x => x.EnumTag == sfx);
            PlayGeneric(taggedSfxClip.AudioClip, sfxAudioSourcePrefab);
        }

        public void PlayMusic(Music music, bool loop = false) {
            TaggedMusicClip taggedMusicClip = musicClipList.First(x => x.enumTag == music);
            PlayGeneric(taggedMusicClip.audioClip, musicAudioSourcePrefab, loop);
        }

        private void PlayGeneric(AudioClip audioClip, GameObject prefab, bool loop = false) {
            GameObject audioPlayer = Instantiate(prefab);
            AudioSource audioSource = audioPlayer.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.loop = loop;
            
            audioSource.Play();
        }
    }
}