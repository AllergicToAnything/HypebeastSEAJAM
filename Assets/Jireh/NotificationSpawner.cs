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
        
    }


   

}
