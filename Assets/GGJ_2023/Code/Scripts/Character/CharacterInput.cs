using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ_2023 {
    public class CharacterInput : MonoBehaviour {
        [SerializeField] private CharacterController2D characterController2D;
        [SerializeField] private PlayerInteract playerInteract;
        
        private float direction;
        private bool heldUp;
        private DefaultControls defaultControls;

        private void Awake() {
            defaultControls = new DefaultControls();
            
            defaultControls.Movement.LeftRight.started += MoveOnstarted;
            defaultControls.Movement.LeftRight.canceled += MoveOnperformed;

            defaultControls.Movement.Jump.performed += JumpOnperformed;
            
            defaultControls.Movement.Interact.performed += InteractOnperformed;
        }

        private void JumpOnperformed(InputAction.CallbackContext obj) {
            heldUp = true;
        }


        private void OnEnable() {
            defaultControls.Enable();
        }

        private void OnDisable() {
            defaultControls.Disable();
        }
        
        private void MoveOnperformed(InputAction.CallbackContext obj) {
            direction = 0;
        }
        
        private void InteractOnperformed(InputAction.CallbackContext obj) {
            playerInteract.Interact();
        }

        private void MoveOnstarted(InputAction.CallbackContext obj) {
            direction = obj.ReadValue<float>();
        }

        private void FixedUpdate() {
            characterController2D.Move(direction, heldUp);
            heldUp = false;
        }
    }
}