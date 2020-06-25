using System;
using UnityEngine;

#pragma warning disable 649
namespace CatAttack
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        private Vector2 m_XFlipVector2 = new Vector2(-1f, 1f);  //multiply this any vector to reverse horizontal component

        [SerializeField] private float m_MaxGroundSpeed = 1f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private Vector2 m_JumpForce = new Vector2(100f, 300f);                  // Amount of force added when the player jumps.
        [SerializeField] private Vector2 m_StarDashForce = new Vector2(100f, 300f);         // Amount of force added when the player jumps.
        [SerializeField] private float m_MaxStarDashSpeed = 2f;         // Amount of force added when the player jumps.
        [SerializeField] private float m_MinimumAirControl = 0.5f;         // minimum air control when airborne
        [SerializeField] private float m_JumpCooldown = 0.1f;         // Time in seconds between jump activations

        //!!!// [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        //[SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Animator;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private SpriteRenderer m_SpriteRenderer;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private StarpowerReservoir m_StarpowerReservoir;

        private bool m_JumpFlag = false;
        private bool m_PreviousJump = false;
        private float m_JumpTimer = 0f;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Animator = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_StarpowerReservoir = GetComponent<StarpowerReservoir>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Animator.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Animator.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool crouch, bool jump)
        {
        //Save a jump only for each lift and press of the button
            if (jump && !m_PreviousJump && m_JumpTimer <= 0)
            {
                m_JumpFlag = true;
            }
            m_PreviousJump = jump;

        //update jump timer
            m_JumpTimer -= Time.deltaTime;

        //if grounded move at ground speed, else apply star dash if available
            float maxXSpeed;
            if (m_Grounded)
            {
                maxXSpeed = m_MaxGroundSpeed;
            }
            else
            {
                maxXSpeed = Mathf.Max(m_MaxStarDashSpeed*m_Animator.GetFloat("StarDashing"), m_MinimumAirControl);
            }

        //if attempting to move in the direction of movement, respect previous momentum
            float targetXSpeed;
            if (Mathf.Sign(move) == Mathf.Sign(m_Rigidbody2D.velocity.x))
            {
                targetXSpeed = Mathf.Sign(move) * Mathf.Max(
                    Mathf.Abs(m_Rigidbody2D.velocity.x),
                    Mathf.Abs(maxXSpeed * move)
                );
            }
            else //if attempting to move in the opposite direction ignore previous momentum
            {
                targetXSpeed = maxXSpeed * move;
            }


                // Move the character
            m_Rigidbody2D.velocity = new Vector2(targetXSpeed, m_Rigidbody2D.velocity.y);

                
            // If the player should jump...
            if (m_JumpFlag && (m_JumpTimer <= 0))
            {
                //perform a jump from the ground 
                if (m_Grounded && m_Animator.GetBool("Ground"))
                {
                   Jump();
                }
                //or dash if airborne and enough stars
                else if (m_StarpowerReservoir.DrainStarpower())
                {
                    StarDashJump();
                }
                m_JumpTimer = m_JumpCooldown;
                m_JumpFlag = false;
            }

            //update sprite orientation
            CheckFlip(move);

            // The Speed animator parameter is set to the absolute value of rigidbody velocity to adjust playback
            m_Animator.SetFloat("Speed", Mathf.Abs(m_Rigidbody2D.velocity.x));
        }

        private void StarDashJump()
        {
            m_Grounded = false;
            m_Animator.SetBool("Ground", false);
            //m_Animator.SetFloat("StarDashing", 1f);
            m_Animator.Play("Base Layer.StarDash", -1, 0f);
            //correct jumpforce for orientation and apply to character
            m_Rigidbody2D.AddForce((m_FacingRight) ? m_StarDashForce : m_StarDashForce * m_XFlipVector2);
        }

        private void Jump()
        {
         // Add a vertical force to the player.
            m_Grounded = false;
            m_Animator.SetBool("Ground", false);
            //correct jumpforce for orientation and apply to character
            m_Rigidbody2D.AddForce((m_FacingRight) ? m_JumpForce : m_JumpForce * m_XFlipVector2);
        }


        private void CheckFlip(float move)
        {
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
        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            m_SpriteRenderer.flipX = !m_FacingRight;


            /*
            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            //*/
        }
    }
}
