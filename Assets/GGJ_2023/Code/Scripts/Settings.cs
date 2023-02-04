using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GGJ_2023 {
    public class Settings : MonoBehaviour {
        public static Settings Instance { get; private set; }

        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private AudioMixer musicMixer;
        [SerializeField] private AudioMixer sfxMixer;

        private void Start()
        {
            musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
            sfxSlider.onValueChanged.AddListener(UpdateSfxVolume);

            musicSlider.value = PlayerPrefs.GetFloat("Music", 0.5f);
            sfxSlider.value = PlayerPrefs.GetFloat("Sfx", 0.5f);
        }

        private void UpdateMusicVolume(float volume)
        {
            if (volume <= 0) {
                sfxMixer.SetFloat("Volume", -80);
                return;
            }
            
            musicMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("Music", volume);
        }

        private void UpdateSfxVolume(float volume)
        {
            if (volume <= 0) {
                sfxMixer.SetFloat("Volume", -80);
                return;
            }

            sfxMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("Sfx", volume);
        }
        
        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
    }
}
