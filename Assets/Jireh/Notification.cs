using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{

    private Camera mainCam;
    private bool isOverNotification;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {

        // Vector3 mousePos = mainCam.ScreenPointToRay();
    }

    private void OnMouseDrag()
    {
        
    }

    private void OnMouseUp()
    {
        
    }
}
