using System;
using TMPro;
using UnityEngine;

namespace GGJ_2023.UI {
    public class ScoreTextUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        private GameManager gameManager;

        private void Start() {
            gameManager = GameManager.Instance;
            gameManager.OnScoreChanged += GameManager_OnScoreChanged;
            
            UpdateUI(gameManager.GetScore());
        }

        private void GameManager_OnScoreChanged(int obj) {
            UpdateUI(obj);
        }

        private void UpdateUI(int obj) {
            textMeshProUGUI.text = $"Score: {obj}";
        }
    }
}
