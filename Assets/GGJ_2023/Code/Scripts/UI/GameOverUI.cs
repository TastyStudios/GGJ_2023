using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ_2023.UI {
    public class GameOverUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI scoreTextMeshProUGUI;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;

        private GameManager gameManager;
        
        private void Start() {
            gameManager = GameManager.Instance;
            
            gameManager.OnGameEnded += GameManagerOnOnGameEnded;
            
            restartButton.onClick.AddListener(RestartButtonOnClick);
            quitButton.onClick.AddListener(QuitButtonOnClick);
        }

        private void QuitButtonOnClick() {
            gameManager.Quit();
        }

        private void RestartButtonOnClick() {
            gameManager.Restart();
        }

        private void GameManagerOnOnGameEnded(int score) {
            Show();
            scoreTextMeshProUGUI.text = $"Score: {score}";
        }
        
        private void Hide() {
            foreach (Transform childTransform in transform) {
                childTransform.gameObject.SetActive(false);
            }
        }

        private void Show() {
            foreach (Transform childTransform in transform) {
                childTransform.gameObject.SetActive(true);
            }
        }
    }
}
