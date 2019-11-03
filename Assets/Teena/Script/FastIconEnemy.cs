using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastIconEnemy : BaseEnemy
{
    Vector3 tempPos = new Vector3();
    bool Stop = false;
    bool DoOnce;
    Vector3 directionVel;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(IsDying == false)
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
        else if(IsDying == true)
        {
            DoDead();
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") == true || col.gameObject.CompareTag("Notification") == true || col.gameObject.CompareTag("Ground") == true)
        {
            if (col.gameObject.CompareTag("Player") == true)
            {
                if (col.gameObject.GetComponent<PlayerController>() != null)
                {
                    col.gameObject.GetComponent<PlayerController>().TakeDamage(enemyData.enemyDamage);
                }
            }

            if (col.gameObject.CompareTag("Notification") == true)
            {
                RandomizeClip();
            }
            IsDying = true;
        }
    }
}