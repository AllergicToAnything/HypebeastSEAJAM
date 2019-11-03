using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSpawner : MonoBehaviour
{

    public GameObject notificationPrefab;
    public AnimationCurve smoothEntry;
    public float notificationDuration;
    public float notificationSpawnTime;
    public float yStart = 6;
    public float yEnd = 4;
    public bool isSpawning = true;

    private Camera mainCam;
    private GameObject notificationClone;
    private Vector3 startPos;
    private Vector3 endPos;

    void Start () {
        mainCam = Camera.main;
        startPos = new Vector3(0, yStart, 10);
        endPos = new Vector3(0, yEnd, 10);
        
        StartCoroutine(SpawnNotifications());
    }
 
     // Update is called once per frame
    void Update () {
        GetWindowExtents();
        // if (Input.GetMouseButtonDown(0)){
        //     StartCoroutine(PushNotification());
        // }
    }

    Vector2 GetWindowExtents(){
        // print("height: " + mainCam.pixelHeight + " and width: " + mainCam.pixelWidth);
        return new Vector2(mainCam.pixelWidth, mainCam.pixelHeight);
    }

    IEnumerator PushNotification(){

        notificationClone = Instantiate(notificationPrefab, startPos, Quaternion.identity, this.transform);
        float lerpProgress = 0;
        while(lerpProgress/notificationDuration < 1){
            lerpProgress += Time.deltaTime;
            // print(lerpProgress);
            float val = smoothEntry.Evaluate(lerpProgress/notificationDuration);
            Vector3 pos = Vector3.Lerp(startPos, endPos, val);
            notificationClone.transform.localPosition = pos;
            yield return null;
        }

        notificationClone = null;
    }

    public IEnumerator SpawnNotifications(){
        float progress = 0;
        
        while (progress < notificationSpawnTime){
            progress += Time.deltaTime;

            if (progress > notificationSpawnTime){
                if (isSpawning)
                StartCoroutine(PushNotification());
                progress = 0;
            }
            yield return null;
        }

        
    }
   

}
