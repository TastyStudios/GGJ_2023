using UnityEngine;

namespace GGJ_2023.Nerves {
    public class NervePoint : MonoBehaviour {
        [field: SerializeField] public NervePointType NervePointType { get; private set; }
        [SerializeField] private GameObject sprite, highlight;

        public void RemoveHighlight() {
            sprite.SetActive(true);
            highlight.SetActive(false);
        }

        public void SetHighlight()
        {
            sprite.SetActive(false);
            highlight.SetActive(true);
        }
    }
}