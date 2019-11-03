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
        Debug.Log(enemyPos);
        enemyPos.y += yOffset;
        Debug.Log(enemyPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (DoOnce == false)
        {
            spriteRenderer.sprite = sprite;
            DoOnce = true;
        }

        if(arrivedToLoc == false)
        {
            /*
            directionVel = target.transform.position - transform.position;
            directionVel.Normalize();
            directionVel *= enemyData.speed;
            */
            //transform.position = Vector3.MoveTowards(transform.position, tempPos, Time.deltaTime * enemyData.speed);

            /*
            float distance = Vector3.Distance(object1.transform.position, object2.transform.position);
            float finalSpeed = (distance / newSpeed);
            transform.position = Vector3.Lerp(obj1, obj2, Time.deltaTime / finalSpeed);
            */

            float step = enemyData.speed * Time.deltaTime;
            tempPos = Vector3.MoveTowards(transform.position, enemyPos, step);
            transform.position = tempPos;

            if(transform.position == enemyPos)
            {
                arrivedToLoc = true;
            }

            
            float distance = Vector3.Distance(tempPos, transform.position);
            if(distance <= 0.0f)
            {
                //arrivedToLoc = true;
            }
            
        }
        else if (arrivedToLoc == true)
        {
            directionVel = Vector3.down * dropSpeed;
            transform.position += directionVel * Time.deltaTime;
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
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

    }
}
