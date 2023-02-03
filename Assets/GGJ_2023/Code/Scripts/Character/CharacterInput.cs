using System;
using GGJ_2023.Character;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace GGJ_2023 {
    public class CharacterInput : MonoBehaviour {
        [SerializeField] private CharacterController2D characterController2D;
        [SerializeField] private CharacterInteract characterInteract;
        
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
            characterInteract.Interact();
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