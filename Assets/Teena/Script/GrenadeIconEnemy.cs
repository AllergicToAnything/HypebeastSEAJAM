using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeIconEnemy : BaseEnemy
{
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    bool DoOnce;
    bool arrivedToLoc = false;
    Vector3 directionVel;
    public float yOffset = 100.0f;
    float dropSpeed = 10.0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        enemyPos = target.transform.position;
        enemyPos.y += yOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDying == false)
        {
            if (DoOnce == false)
            {
                spriteRenderer.sprite = sprite;
                DoOnce = true;
            }

            if (arrivedToLoc == false)
            {
                float step = enemyData.speed * Time.deltaTime;
                tempPos = Vector3.MoveTowards(transform.position, enemyPos, step);
                transform.position = tempPos;

                if (transform.position == enemyPos)
                {
                    arrivedToLoc = true;
                }
            }
            else if (arrivedToLoc == true)
            {
                directionVel = Vector3.down * dropSpeed;
                transform.position += directionVel * Time.deltaTime;
            }
        }
        else if (IsDying == true)
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
