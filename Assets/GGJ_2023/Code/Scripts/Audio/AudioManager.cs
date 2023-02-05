using System.Collections;
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

        private AudioSource _prevSource;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                Debug.LogError("There are more than one AudioManagers in this scene.");
                return;
            }

            Instance = this;
        }

        public void PlaySfx(Sfx sfx, Vector2 position) {
            TaggedSfxClip taggedSfxClip = sfxClipList.First(x => x.EnumTag == sfx);
            PlayGeneric(taggedSfxClip.AudioClip, position, sfxAudioSourcePrefab);
        }

        public void PlayMusic(Music music, Vector2 position) {
            Debug.Log($"Playing BGM {music}");
            TaggedMusicClip taggedMusicClip = musicClipList.First(x => x.enumTag == music);

            if(_prevSource != null) StartCoroutine(FadeOut(_prevSource));
            _prevSource = PlayGeneric(taggedMusicClip.audioClip, position, musicAudioSourcePrefab, true);
            FadeIn(_prevSource);
        }

        private IEnumerator FadeIn(AudioSource audioSource)
        {
            for (var volume = 0f; volume < 1; volume -= Time.deltaTime * 4)
            {
                audioSource.volume = volume;
                yield return null;
            }

            audioSource.volume = 1;
        }

        private IEnumerator FadeOut(AudioSource audioSource)
        {
            for(var volume = 1f; volume > 0; volume -= Time.deltaTime * 4)
            {
                audioSource.volume = volume;
                yield return null;
            }

            Destroy(audioSource.gameObject);
        }

        private AudioSource PlayGeneric(AudioClip audioClip, Vector2 position, GameObject prefab, bool loop = false) {
            GameObject audioPlayer = Instantiate(prefab);
            audioPlayer.transform.position = position;
            AudioSource audioSource = audioPlayer.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.loop = loop;
            
            audioSource.Play();
            return audioSource;
        }
    }
}