using GGJ_2023.Nerves;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ_2023
{
    [CreateAssetMenu(fileName = "Scenario", menuName = "Scenario", order = 1)]
    class Scenario : ScriptableObject
    {
        [field: SerializeField] public List<NerveConnection> NervePoints { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
