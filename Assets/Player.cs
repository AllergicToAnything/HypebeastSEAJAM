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
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            playerImmortal = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            playerImmortal = false;
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            playerController.runSpeed *= 1.5f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            playerController.runSpeed /= 1.5f;
        }
    }

}
