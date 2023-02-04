using System;
using UnityEngine;

namespace GGJ_2023.Character {
    public class CharacterAnimatorController : MonoBehaviour {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController2D characterController2D;
        [SerializeField] private CharacterInteract characterInteract;
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Grab = Animator.StringToHash("Grab");

        private void Start() {
            characterController2D.OnJump += CharacterController2DOnJump;
            characterController2D.OnLand += CharacterController2DOnOnLand;
            characterController2D.OnMove += CharacterController2DOnOnMove;

            characterInteract.OnGrabChanged += CharacterInteractOnOnGrabChanged;
        }

        private void CharacterInteractOnOnGrabChanged(bool obj) {
            if (!obj) return;
            
            animator.SetTrigger(Grab);
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

        private void CharacterController2DOnJump() {
            // Might not need
        }
    }
}
