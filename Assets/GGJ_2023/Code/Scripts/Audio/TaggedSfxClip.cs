using UnityEngine;

namespace GGJ_2023.Audio {
    [System.Serializable]
    public struct TaggedSfxClip {
        public Sfx EnumTag;
        public AudioClip AudioClip;
    }
}