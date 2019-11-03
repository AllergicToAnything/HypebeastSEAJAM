using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public float xVelocity = 5;
    public float yVelocity = 5;
    public PlayerController playerController;

   

    public void Projectile()
    {
        Vector2 playerVel = playerController.GetComponent<Rigidbody2D>().velocity;
        if (playerController.m_FacingRight)
        {
            
            rb.velocity = new Vector2(xVelocity, yVelocity) + playerVel;
        }
        if (!playerController.m_FacingRight)
        {
            rb.velocity = new Vector2(-xVelocity, yVelocity) + playerVel;
        }
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject!=this.gameObject||collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        Destroy(gameObject);
    }

}
