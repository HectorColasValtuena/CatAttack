using System;
using System.Collections;
using UnityEngine;

#pragma warning disable 649
namespace CatAttack
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        const float k_GroundedRadius = .02f; // Radius of the overlap circle to determine if grounded
        //const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up

        private Vector2 m_XFlipVector2 = new Vector2(-1f, 1f);  //multiply this any vector to reverse horizontal component

        [SerializeField] private float m_MaxGroundSpeed = 1f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private Vector2 m_JumpSpeed = new Vector2(2f, 3f);  // Initial speed of a jump
        [SerializeField] private Vector2 m_StarDashSpeed = new Vector2(2f, 4f);  // Initial speed of a dash
        [SerializeField] private float m_MaxStarDashSpeed = 2f;         // Amount of force added when the player jumps.
        [SerializeField] private float m_MinimumAirControl = 0.5f;         // minimum air control when airborne
        [SerializeField] private float m_JumpCooldown = 0.1f;         // Time in seconds between jump activations

        //!!!// [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        //[SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        [SerializeField] private Transform[] m_GroundChecks;    // A position marking where to check if the player is grounded.
        [SerializeField] public Transform m_CameraTarget;    // A position marking where to check if the player is grounded.
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Animator m_Animator;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private SpriteRenderer m_SpriteRenderer;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private StarpowerReservoir m_StarpowerReservoir;

        private bool m_JumpFlag = false;
        private bool m_PreviousJump = false;
        private float m_JumpTimer = 0f;

        [System.NonSerialized] public bool m_ControlDisabled = false;  //are player controls disabled?
        [System.NonSerialized] public bool m_CatDead = false;          //is the player dead?
        [System.NonSerialized] public bool m_IsAsleep = false;          //is the player asleep?

        public bool m_CameraFocused { get { return m_IsAsleep; } }      //return true if camera needs to focus 

        private void Awake()
        {
            //store a reference to this controller in the level manager
            LevelManager.playerGameObject = this;

            // Setting up references.
            m_Animator = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_StarpowerReservoir = GetComponent<StarpowerReservoir>();
        }


        private void FixedUpdate()
        {
            //check and store wether we¡re touching ground
            m_Grounded = CheckGround ();
            //regain starpower if touching the ground
            if (m_Grounded) { m_StarpowerReservoir.RegenerateStarpower(); }

            // Set animator variables to drive animation
            m_Animator.SetBool("Ground", m_Grounded);
            m_Animator.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            //if dropped below death height, die.
            if (transform.position.y < LevelManager.instance.lethalDropoffHeight) { Death(); }
        }

        private bool CheckGround ()
        {
            foreach (Transform groundCheck in m_GroundChecks)
            {
                Collider2D collision = Physics2D.OverlapCircle(groundCheck.position, k_GroundedRadius, m_WhatIsGround);
                if (collision != null)
                {
                    return true;
                }
            }
            return false;
        }

        public void Move(float move, bool crouch, bool jump)
        {
            if (m_ControlDisabled || m_CatDead || m_IsAsleep) { return; } //ignore inputs if controls are disabled or character is dead

            //update sprite orientation
            CheckFlip(move);

            //if moving reset camera focus back to the player
            if (LevelManager.cameraFocusTarget != null) { LevelManager.cameraFocusTarget = null; }

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
            float movementSign = m_FacingRight ? 1 : -1;
            if (movementSign == Mathf.Sign(m_Rigidbody2D.velocity.x) || move == 0)
            {
                targetXSpeed = movementSign * Mathf.Max(
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
                   Jump(move);
                }
                //or dash if airborne and enough stars
                else if (m_StarpowerReservoir.DrainStarpower())
                {
                    StarDashJump(move);
                }
                m_JumpTimer = m_JumpCooldown;
                m_JumpFlag = false;
            }


            // The Speed animator parameter is set to the absolute value of rigidbody velocity to adjust playback
            m_Animator.SetFloat("Speed", Mathf.Abs(m_Rigidbody2D.velocity.x));
        }

        private void StarDashJump(float move)
        {
            m_Grounded = false;
            m_Animator.SetBool("Ground", false);

            //force restart the stardash animation state
            m_Animator.Play("Base Layer.StarDash", -1, 0f);

            //Set the rigidbody's vertical velocity to dash speed, and scale horizontal velocity with input
            //always dash full speed in the direction the player is facing
            m_Rigidbody2D.velocity = (m_FacingRight) ? (m_StarDashSpeed) : (m_StarDashSpeed * m_XFlipVector2);
        }

        private void Jump(float move)
        {
         // Add a vertical force to the player.
            m_Grounded = false;
            m_Animator.SetBool("Ground", false);


            //Set the rigidbody's vertical velocity to jump speed, and scale horizontal velocity with input
            m_Rigidbody2D.velocity = new Vector2 (m_JumpSpeed.x * move, m_JumpSpeed.y);  
        }

        private void CheckFlip(float move)
        { // If the input is moving the player right and the player is facing left...
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
        {   //flip the character
            m_FacingRight = !m_FacingRight;
            m_SpriteRenderer.flipX = !m_FacingRight;
        }

        //resets the player to target position. if no target position given, get default respawn spot from the level manager
        public void ResetPlayer () { ResetPlayer(LevelManager.instance.respawnSpot); }
        public void ResetPlayer (Vector3 targetPosition)
        {
            //reset controls disabled and special states
            m_CatDead = false;
            m_IsAsleep = false;
            DeImmobilize();
            
            //reset our momentum and position
            m_Rigidbody2D.velocity = Vector2.zero;
            transform.position = targetPosition;

            //reset jump input
            m_JumpFlag = false;

            //regenerate Star Power            
            m_StarpowerReservoir.RegenerateStarpower();

            //remove dead flag from animator
            m_Animator.SetBool("Dead", false);
            m_Animator.SetBool("Sleeping", false);

            //force animator to reset back to entry
            m_Animator.Play("Base Layer.Standing");
        }

        public IEnumerator DelayedReset (float timeDelay = 1)
        {
            yield return new WaitForSeconds(timeDelay);
            ResetPlayer();
        }

        //kill the player character. Automatically resets the player.
        public void Death () 
        {
            if (m_CatDead == true) return;  //if already dead, ignore
            Immobilize();
            m_CatDead = true;                   //store death state to disable controls
            m_Animator.SetBool("Dead", true);   //indicate the animator we're dead
            StartCoroutine(DelayedReset());    //reset the player after X seconds
        }

        //put the cat to sleep
        public void FallAsleep ()
        {
            Immobilize();
            m_Rigidbody2D.simulated = true;
            LevelManager.cameraFocusTarget = transform;
            m_IsAsleep = true;
            m_Animator.SetBool("Sleeping", true);
            m_Animator.Play("Base Layer.BeginSleep");
        }

        //Disable controls and physics
        public void Immobilize ()
        {
            m_Rigidbody2D.simulated = false;
            m_Rigidbody2D.velocity = Vector2.zero;
            m_ControlDisabled = true;
        }
        //re-enable controls and physics
        public void DeImmobilize ()
        {
            m_Rigidbody2D.simulated = true;
            m_ControlDisabled = false;
        }
    }
}
