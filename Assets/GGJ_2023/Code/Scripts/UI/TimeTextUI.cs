using System;
using TMPro;
using UnityEngine;

namespace GGJ_2023.UI {
    public class TimeTextUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        
        private GameManager gameManager;

        private void Start() {
            gameManager = GameManager.Instance;
            
            UpdateUI(gameManager.GetTime());
            
            gameManager.OnTimeTick += GameManager_OnTimeTick;
        }

        private void GameManager_OnTimeTick(float obj) {
            UpdateUI(obj);
        }

        private void UpdateUI(float obj) {
            textMeshProUGUI.text = $"Time: {obj.ToString("F1")}";
        }
    }
}
