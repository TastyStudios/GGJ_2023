using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace GGJ_2023
{
    public class CharacterController2D : MonoBehaviour
    {
        [SerializeField] private float m_JumpForce = 450;                           // Amount of force added when the player jumps.
        [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
        [SerializeField] private float speed = 100;
        [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
        [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
        [SerializeField] private CharacterInput characterInput;

        [SerializeField] private PhysicsMaterial2D _airMaterial, _groundMaterial;

        const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private Vector3 m_Velocity = Vector3.zero;
        private float m_jumpIdleUntil;

        public event Action OnLand;
        public event Action OnJump;
        public event Action<float, bool> OnMove;

        [System.Serializable]
        public class BoolEvent : UnityEvent<bool> { }


        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            bool wasGrounded = m_Grounded;
            DoAirCheck(wasGrounded);
            if (!wasGrounded && m_Grounded)
            {
                OnLand.Invoke();
                m_Rigidbody2D.sharedMaterial = _airMaterial;
            }
            else if (wasGrounded && !m_Grounded)
            {
                m_Rigidbody2D.sharedMaterial = _groundMaterial;
            }
        }

        private void DoAirCheck(bool wasGrounded)
        {
            m_Grounded = false;

            if (!wasGrounded && m_Rigidbody2D.velocity.y > 0 || m_jumpIdleUntil > Time.fixedTime)
            {
                return;
            }

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    break;
                }
            }
        }

        public void Move(float move, bool jump)
        {
            OnMove?.Invoke(move, jump);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {

                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * speed, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_jumpIdleUntil = Time.fixedTime + .2f;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                OnJump?.Invoke();
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}