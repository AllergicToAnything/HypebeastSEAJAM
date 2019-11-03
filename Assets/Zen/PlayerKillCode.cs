using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillCode : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") == true)
        {
            if (!collision.gameObject.GetComponent<Player>().playerImmortal)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
