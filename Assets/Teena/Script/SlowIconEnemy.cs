using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowIconEnemy : BaseEnemy
{
    public float amplitude = 0.5f;
    public float frequency = 1f;
    Vector3 tempPos = new Vector3();
    bool Stop = false;
    bool DoOnce = false;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(DoOnce == false)
        {
            spriteRenderer.sprite = sprite;
        }

        if (Stop == false && target != null)
        {
            float step = enemyData.speed * Time.deltaTime;
            tempPos = Vector3.MoveTowards(transform.position, target.transform.position, step);
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
            transform.position = tempPos;
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
