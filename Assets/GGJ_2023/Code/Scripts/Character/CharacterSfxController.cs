using System;
using GGJ_2023.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GGJ_2023.Character {
    public class CharacterSfxController : MonoBehaviour {
        [SerializeField] private CharacterController2D characterController;

        private void Start() {
            characterController.OnJump += CharacterControllerOnOnJump;
            characterController.OnLand += CharacterControllerOnOnLand;
        }

        private void CharacterControllerOnOnLand() {
            AudioManager.Instance.PlaySfx(Sfx.Landing, characterController.transform.position);
        }

        private void CharacterControllerOnOnJump() {
            Sfx[] jumpSfxArray = { Sfx.Jump1, Sfx.Jump2 };
            int randIndex = Random.Range(0, jumpSfxArray.Length);
            
            AudioManager.Instance.PlaySfx(jumpSfxArray[randIndex], characterController.transform.position);
        }
    }
}
