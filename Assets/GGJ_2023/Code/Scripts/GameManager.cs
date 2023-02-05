using System;
using GGJ_2023.Nerves;
using System.Collections.Generic;
using System.Linq;
using GGJ_2023.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using System.Collections;

namespace GGJ_2023
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public event Action<int> OnGameEnded;
        public event Action<Scenario> OnScenarioChanged;
        public event Action<float> OnTimeTick;
        public event Action<int> OnScoreChanged;

        [SerializeField]
        private SpriteRenderer screen;
        [SerializeField]
        private List<Scenario> scenarios;

        [SerializeField]
        private GameObject _goodScreen, _badScreen, _correct, _wrong, _warning;

        private Scenario currentScenario;
        private int points;
        private float time;

        private bool _somethingWrong;

        [SerializeField]
        private List<BodyPain> _painList;
        private Dictionary<NervePointType, BodyPain> _pains;
        private Music prevMusic;

        private void Start()
        {
            if (Instance != null)
            {
                Debug.LogError("There are more than one GameManagers.");
                Destroy(gameObject);
                return;
            }

            _pains = _painList.ToDictionary(pain => pain.BodyPart);

            Instance = this;

            prevMusic = Music.None;
            StartCoroutine(CorrectThenNewScenario());
        }

        private void Update()
        {
            if (_somethingWrong)
            {
                time -= Time.deltaTime;
                OnTimeTick?.Invoke(time);
            }

            if (time < 0)
            {
                OnGameEnded?.Invoke(points);
                gameObject.SetActive(false);
                return;
            }
        }

        private IEnumerator Popup(GameObject obj)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            obj.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            obj.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            obj.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            obj.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            obj.SetActive(false);
        }

        private IEnumerator PopupShort(GameObject obj)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            obj.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            obj.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            obj.SetActive(false);
        }

        private IEnumerator CorrectThenNewScenario()
        {
            time = 30;
            var difficulty = points / 3 + 1;
            var music = (Music)(1 + Mathf.Min((difficulty - 1) / 2 + 1, 5));
            if (music != prevMusic)
            {
                prevMusic = music;
                var am = AudioManager.Instance;
                if (am)
                {
                    am.PlayMusic(music, Vector2.zero);
                }
            }

            if (difficulty > 7)
            {
                time -= difficulty - 7;
            }
            _somethingWrong = false;
            foreach (var part in _painList)
            {
                part.Hide();
            }

            _goodScreen.SetActive(true);
            _badScreen.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            _goodScreen.SetActive(false);
            _badScreen.SetActive(true);
            StartCoroutine(Popup(_warning));
            ChooseRandomScenario(difficulty);
        }

        private void ChooseRandomScenario(int difficulty)
        {
            /*var r = Random.Range(0, scenarios.Count);
            currentScenario = scenarios[r];
            Debug.Log($"Loading scenario #{r} ({currentScenario.NervePoints[0].BodyPart} -> {currentScenario.NervePoints[0].Sense})");
            */

            currentScenario = ScriptableObject.CreateInstance<Scenario>();

            var bodyParts = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            for (var i = 0; i < difficulty && i < bodyParts.Count; i++)
            {
                var r = Random.Range(0, bodyParts.Count);
                var bodyPart = (NervePointType)bodyParts[r];
                bodyParts.RemoveAt(r);

                var sense = (NervePointType)Random.Range(100, 103);

                _pains[bodyPart].SetPain(sense);
                currentScenario.NervePoints.Add(new NerveConnection(bodyPart, sense));
            }

            foreach (var bodypart in bodyParts)
            {
                _pains[(NervePointType)bodypart].Hide();
            }

            time = 30;
            if (difficulty > 7)
            {
                time -= difficulty - 7;
            }
            _somethingWrong = true;

            OnScenarioChanged?.Invoke(currentScenario);
        }


        public void CompleteChain(List<NervePoint> nervePoints)
        {
            Debug.Log("CompleteChain");
            if (CheckChain(nervePoints))
            {
                IncrementScore();
                AudioManager.Instance.PlaySfx(Sfx.Victory, Vector2.zero);
                StartCoroutine(CorrectThenNewScenario());
                StartCoroutine(PopupShort(_correct));
            }
            else
            {
                AudioManager.Instance.PlaySfx(Sfx.Fail, Vector2.zero);
                StartCoroutine(Popup(_wrong));
            }
        }

        private void IncrementScore()
        {
            points++;
            OnScoreChanged?.Invoke(points);
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

        public Scenario GetCurrentScenario()
        {
            return currentScenario;
        }

        public float GetTime()
        {
            return time;
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public int GetScore()
        {
            return points;
        }
    }
}
