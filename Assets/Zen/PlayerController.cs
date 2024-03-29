﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 200f;                          // Amount of force added when the player jumps.
    private float initJF;
    [SerializeField]
    private float jumpCD = 1;
    private bool addJumpForce = false;
    private float initJUMPCD;
    public int hp = 20;
    [SerializeField] private float maxJumpForce = 50f;
    [SerializeField] private float addForcePerSecond = 1f;
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    public bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    Animator animator;

    public float runSpeed = 40f;
    float initRunSpeed;
    bool jump = false;    
    float horizontalMove = 0f;

    public bool ableToAttack = true;
    bool attack = false;
    public float attackCD = .8f;
    float initAttackCD;
    public GameObject bulletSpawner;
    public PlayerBullet playerBullet;
    public Image hpImage;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
        initJF = m_JumpForce;
        initJUMPCD = jumpCD;
        initRunSpeed = runSpeed;
        initAttackCD = attackCD;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
        Move(horizontalMove * Time.fixedDeltaTime, false, jump);
    }


    private void Update()
    {
        if(hpImage != null)
        {
            if (hp > 0)
            {
                float hpFill = hp / 20.0f;
                Debug.Log(hpFill);
                hpImage.fillAmount = hpFill;
            }
            else if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (Input.GetButton("Jump"))
        {

            if (jumpCD > 0)
            {
                jumpCD -= Time.deltaTime;
                if (addForcePerSecond < maxJumpForce)
                {
                    if (!addJumpForce)
                    {
                        m_JumpForce += addForcePerSecond * Time.deltaTime;
                    }
                    if (m_Grounded)
                    {
                        jump = true;
                        if (jump)
                        {
                            addJumpForce = true;
                            Move(horizontalMove * Time.fixedDeltaTime, false, jump);

                        }
                    }
                }
            }
            

        }

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (m_Grounded)
        {
            if (Input.GetButton("Jump"))
            {
                animator.Play("PlayerJumpAnticipation");
            }

        
        }
        if (Input.GetButtonUp("Jump"))
        {
            
                jump = false;
                jumpCD = initJUMPCD;
                m_JumpForce = initJF;
               addJumpForce = false;
            
            
        }
        if (!m_Grounded)
        {
            m_Rigidbody2D.gravityScale += .05f;
        }

        if (m_Grounded)
        {
            m_Rigidbody2D.gravityScale = 1;
            if (Input.GetAxis("Horizontal") != 0&&!attack)
            {
                animator.Play("PlayerWalking");
            }
            if (Input.GetAxis("Horizontal") == 0&&!attack&& !Input.GetMouseButton(0))
            {
                animator.Play("PlayerIdle");
            }
        }
        else if (!m_Grounded && !attack)
        {
            animator.Play("PlayerJumpHang");
        }
        

        if (ableToAttack)
        {
            if (attackCD > 0)
            {
                attackCD -= Time.deltaTime;
            }
            if (Input.GetMouseButton(0))
            {
                if(Input.GetAxis("Horizontal") == 0)
                {
                    animator.Play("PlayerAttack");
                }
                if (attackCD <= 0)
                {
                    attack = true;
                }
                if (attack)
                {
                    Attack();
                    if (Input.GetAxis("Horizontal") != 0)
                    {
                        animator.Play("PlayerAttack");
                    }

                }
            }
        }

    }

    

    public List<PlayerBullet> bullets = new List<PlayerBullet>();

    public void Attack()
    {

        GameObject bullet = Instantiate(playerBullet.gameObject, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
        bullets.Add(bullet.GetComponent<PlayerBullet>());
        foreach (PlayerBullet r in bullets)
        {
            PlayerBullet pBullet = bullet.GetComponent<PlayerBullet>();

            if(pBullet)
                pBullet.playerController = this;
                pBullet.Projectile();

        }
        if (bullets.Count > 1)
        {
            bullets.RemoveAt(0);
        }

        attackCD = initAttackCD;
        attack = false;

    }

    void Die()
    {

    }

   

    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
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
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    public void TakeDamage(int dmgTaken)
    {
        hp -= dmgTaken;
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