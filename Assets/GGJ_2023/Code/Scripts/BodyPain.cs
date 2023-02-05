using GGJ_2023.Nerves;
using UnityEngine;

namespace GGJ_2023
{
    public class BodyPain : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField] private Sprite _cold, _hot, _electric;

        [field: SerializeField] public NervePointType BodyPart { get; private set; }

        public void SetPain(NervePointType pain)
        {
            _spriteRenderer.sprite = pain switch
            {
                NervePointType.Cold => _cold,
                NervePointType.Heat => _hot,
                NervePointType.Electric => _electric,
                _ => null,
            };
            _spriteRenderer.enabled = true;
        }

        public void Hide()
        {
            _spriteRenderer.enabled = false;
        }
    }
}
