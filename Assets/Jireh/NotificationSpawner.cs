using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSpawner : MonoBehaviour
{

    public GameObject notificationPrefab;
    public float notificationSpawnTime;
    
    public GameObject pointer;




    private Camera mainCam;

    void Start () {
        mainCam = Camera.main;
    }
 
     // Update is called once per frame
    void Update () {
     //This prevents your thrown object from ascending to infinity and beyond. Disable if you're trying to throw Buzz Lightyear.
        // if (force.y > 0.0f) {
        //     force.y -= 0.1f;
        // }
        Vector3 screenToWorldPoint = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 screenToWorldPointFixed = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
        pointer.transform.position = screenToWorldPointFixed;
    }


   

}
