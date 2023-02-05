using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonUI : MonoBehaviour {
    [SerializeField] private Button button;

    private void Start() {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick() {
        StartCoroutine(LoadGameRoutine());
    }

    private IEnumerator LoadGameRoutine() {
        AsyncOperation loadingScreenLoad = SceneManager.LoadSceneAsync("LoadingScreen");
        AsyncOperation gameLoad = SceneManager.LoadSceneAsync("Game");

        yield return new WaitWhile(() => !gameLoad.isDone);

        SceneManager.UnloadSceneAsync("LoadingScreen");
    }
}
