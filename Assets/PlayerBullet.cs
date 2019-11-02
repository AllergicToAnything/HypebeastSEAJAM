using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public float xVelocity = 5;
    public float yVelocity = 5;
    public PlayerController playerController;
    public Collider2D thisCollider;

    private void Start()
    {
        Projectile();
    }


    void Projectile()
    {
        if (playerController.m_FacingRight)
        {
            rb.velocity = new Vector2(xVelocity, yVelocity);
        }
        if (!playerController.m_FacingRight)
        {
            rb.velocity = new Vector2(-xVelocity, yVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {if(collision.gameObject!=this.gameObject)
        Destroy(gameObject);
    }

}
