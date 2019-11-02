using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSpawner : MonoBehaviour
{

    public GameObject notificationPrefab;
    public float notificationSpawnTime;
    
    public GameObject pointer;

    void Start () {
         
     }
 
     // Update is called once per frame
    void Update () {
     //This prevents your thrown object from ascending to infinity and beyond. Disable if you're trying to throw Buzz Lightyear.
        if (force.y > 0.0f) {
            force.y -= 0.1f;
            }
    }


    public Vector3 gameObjectSreenPoint;
    public Vector3 mousePreviousLocation;
    public Vector3 mouseCurLocation;

    void OnMouseDown() {
        //This grabs the position of the object in the world and turns it into the position on the screen
        gameObjectSreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //Sets the mouse pointers vector3
        mousePreviousLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
    }
 
    public Vector3 force;
    public Vector3 objectCurrentPosition;
    public Vector3 objectTargetPosition;
    public float topSpeed = 10;
    void OnMouseDrag() {
        mouseCurLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
        force = mouseCurLocation - mousePreviousLocation;//Changes the force to be applied
        mousePreviousLocation = mouseCurLocation;
    }
 
    public void OnMouseUp() {
    //Makes sure there isn't a ludicrous speed
    if (GetComponent<Rigidbody>().velocity.magnitude > topSpeed)
        force = GetComponent<Rigidbody>().velocity.normalized * topSpeed;
    }
 
    public void FixedUpdate() {
        GetComponent<Rigidbody>().velocity = force;
    }

}
