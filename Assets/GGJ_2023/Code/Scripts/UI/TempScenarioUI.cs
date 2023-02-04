using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace GGJ_2023.UI {
    public class TempScenarioUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        private GameManager gameManager;

        private void Start() {
            gameManager = GameManager.Instance;
            
            gameManager.OnScenarioChanged += GameManagerOnOnScenarioChanged;
            
            RefreshUI();
        }

        private void GameManagerOnOnScenarioChanged(Scenario obj) {
            RefreshUI(obj);
        }

        private void RefreshUI(Scenario scenario = null) {
            if (scenario == null) {
                scenario = gameManager.GetCurrentScenario();
            }

            textMeshProUGUI.text = scenario.ToString();
        }
    }
}
