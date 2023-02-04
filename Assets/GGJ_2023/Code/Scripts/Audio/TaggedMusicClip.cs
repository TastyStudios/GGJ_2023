using UnityEngine;
using UnityEngine.Serialization;

namespace GGJ_2023.Audio {
    [System.Serializable]
    public struct TaggedMusicClip {
        public Music enumTag;
        public AudioClip audioClip;

        public override string ToString() {
            return enumTag.ToString();
        }
    }
}