using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ_2023 {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        private void Awake() {
            if (Instance != null) {
                Debug.LogError("There are more than one GameManagers.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }


        public void CompleteChain(List<NervePoint> nervePoints) {
            Debug.Log("CompleteChain");
        }
    }
}
