using System;
using GGJ_2023.Nerves;
using System.Collections.Generic;
using System.Linq;
using GGJ_2023.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace GGJ_2023
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public event Action<int> OnGameEnded;
        public event Action<Scenario> OnScenarioChanged; 

        [SerializeField]
        private SpriteRenderer screen;
        [SerializeField]
        private List<Scenario> scenarios;

        private Scenario currentScenario;
        private int points;
        private float time;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There are more than one GameManagers.");
                Destroy(gameObject);
                return;
            }

            Instance = this;

            time = 120;
            
            ChooseRandomScenario();
        }

        private void Update() {
            time -= Time.deltaTime;

            if (time < 0) {
                OnGameEnded?.Invoke(points);
                gameObject.SetActive(false);
                return;
            }
        }

        private void ChooseRandomScenario()
        {
            var r = Random.Range(0, scenarios.Count);
            currentScenario = scenarios[r];
            Debug.Log($"Loading scenario #{r} ({currentScenario.NervePoints[0].BodyPart} -> {currentScenario.NervePoints[0].Sense})");
            screen.sprite = currentScenario.Sprite;

            time += 10;
            
            OnScenarioChanged?.Invoke(currentScenario);
        }


        public void CompleteChain(List<NervePoint> nervePoints)
        {
            Debug.Log("CompleteChain");
            if (CheckChain(nervePoints))
            {
                points++;
                AudioManager.Instance.PlaySfx(Sfx.Victory, Vector2.zero);
                Debug.Log($"Success {points}");
                ChooseRandomScenario();
            }
            else
            {
                AudioManager.Instance.PlaySfx(Sfx.Fail, Vector2.zero);
                Debug.Log("Failed");
                ChooseRandomScenario();
            }
        }

        private bool CheckChain(List<NervePoint> nervePoints)
        {
            var wantedConnections = currentScenario.NervePoints.ToHashSet();
            for (var i = 0; i < nervePoints.Count - 1; i += 2)
            {
                var left = nervePoints[i].NervePointType;
                var right = nervePoints[i + 1].NervePointType;
                if (wantedConnections.Remove(new NerveConnection(left, right)) || wantedConnections.Remove(new NerveConnection(right, left)))
                {
                    continue;
                }
                Debug.Log($"Incorrect connection {left} -> {right}");
                return false;
            }

            Debug.Log($"Missing {wantedConnections.Count} connections");
            return wantedConnections.Count == 0;
        }

        public Scenario GetCurrentScenario() {
            return currentScenario;
        }

        public float GetTime() {
            return time;
        }
        
        public void Restart() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit() {
            Application.Quit();
        }
    }
}
