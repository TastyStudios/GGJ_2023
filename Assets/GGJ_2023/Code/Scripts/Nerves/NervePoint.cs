using UnityEngine;

namespace GGJ_2023.Nerves {
    public class NervePoint : MonoBehaviour {
        [field: SerializeField] public NervePointType NervePointType { get; private set; }
        [SerializeField] private GameObject highlight;

        public void RemoveHighlight() {
            highlight.SetActive(false);
        }

        public void SetHighlight() {
            highlight.SetActive(true);
        }
    }
}