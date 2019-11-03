using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastIconEnemy : BaseEnemy
{
    public float amplitude = 0.5f;
    public float frequency = 1f;
    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    bool Stop = false;
    bool DoOnce;
    Vector3 directionVel;
    public float DeathTimer;

    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, DeathTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (DoOnce == false)
        {
            tempPos = target.transform.position;
            directionVel = target.transform.position - transform.position;
            directionVel.Normalize();
            directionVel *= enemyData.speed;
            spriteRenderer.sprite = sprite;
            DoOnce = true;
        }

        if (Stop == false && DoOnce == true)
        {
            transform.position += directionVel * Time.deltaTime;
        }
    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player") == true || col.gameObject.CompareTag("Notification") == true || col.gameObject.CompareTag("Ground") == true)
        {
            if (col.gameObject.CompareTag("Player") == true)
            {
                if(col.gameObject.GetComponent<PlayerController>() != null)
                {
                    col.gameObject.GetComponent<PlayerController>().TakeDamage(enemyData.enemyDamage);
                }
            }

            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

    }
}