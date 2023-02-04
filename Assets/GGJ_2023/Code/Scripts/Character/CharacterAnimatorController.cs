using System;
using UnityEngine;

namespace GGJ_2023.Character {
    public class CharacterAnimatorController : MonoBehaviour {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController2D characterController2D;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int Jump = Animator.StringToHash("Jump");

        private void Start() {
            characterController2D.OnJump += CharacterController2DOnOnJump;
            characterController2D.OnLand += CharacterController2DOnOnLand;
            characterController2D.OnMove += CharacterController2DOnOnMove;
        }

        private void CharacterController2DOnOnMove(float leftRight, bool jump) {
            animator.SetBool(IsWalking, leftRight != 0);

            if (jump) {
                animator.SetTrigger(Jump);
            }
        }

        private void CharacterController2DOnOnLand() {
            // Might not need
        }

        private void CharacterController2DOnOnJump() {
            
        }
    }
}
