using GGJ_2023.Nerves;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GGJ_2023
{
    [CreateAssetMenu(fileName = "Scenario", menuName = "Scenario", order = 1)]
     public class Scenario : ScriptableObject
    {
        [field: SerializeField] public List<NerveConnection> NervePoints { get; private set; } = new List<NerveConnection>();
        [field: SerializeField] public Sprite Sprite { get; private set; }

        public override string ToString() {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (NerveConnection nerveConnection in NervePoints) {
                stringBuilder.Append(nerveConnection.BodyPart.ToString());
                stringBuilder.Append(" -> ");
                stringBuilder.Append(nerveConnection.Sense.ToString());
                stringBuilder.Append(" -> Brain");
            }

            return stringBuilder.ToString();
        }
    }
}
