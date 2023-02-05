using GGJ_2023.Audio;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ_2023.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTextMeshProUGUI;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private GameObject topBar;

        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameManager.Instance;

            Hide();

            gameManager.OnGameEnded += GameManagerOnOnGameEnded;

            restartButton.onClick.AddListener(RestartButtonOnClick);
            quitButton.onClick.AddListener(QuitButtonOnClick);
        }

        private void QuitButtonOnClick()
        {
            gameManager.Quit();
        }

        private void RestartButtonOnClick()
        {
            gameManager.Restart();
        }

        private void GameManagerOnOnGameEnded(int score)
        {
            Show();
            scoreTextMeshProUGUI.text = $"Score: <b><color=yellow>{score}";
            AudioManager.Instance.PlayMusic(Music.HowItBegins, Vector2.zero);
        }

        private void Hide()
        {
            foreach (Transform childTransform in transform)
            {
                childTransform.gameObject.SetActive(false);
            }
            topBar.SetActive(true);
        }

        private void Show()
        {
            foreach (Transform childTransform in transform)
            {
                childTransform.gameObject.SetActive(true);
            }
            topBar.SetActive(false);
        }
    }
}
