using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public bool playerImmortal = false;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerImmortal = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            playerImmortal = false;
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            playerController.runSpeed *= 1.5f;
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            playerController.runSpeed /= 1.5f;
        }
    }

}
