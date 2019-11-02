using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public float notificationShrinkageRatio = 0.2f;
    public float shrinkSpeed = 0.1f;
    public float scaleThreshold = 0.1f;

    private Camera mainCam;
    private bool isOverNotification;
    private Vector3 mousePreviousLocation;
    private Vector3 velocity;
    private Vector3 objectCurrentPosition;
    private Vector3 objectTargetPosition;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {   
        
    }

     void OnMouseDown() {
        //This grabs the position of the object in the world and turns it into the position on the screen
        //Sets the mouse pointers vector3
        mousePreviousLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        StartCoroutine(Shrink());
    }

    // void OnMouseDrag() {
    //     Vector3 mouseCurLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    //     Vector3 screenToWorldPoint = mainCam.ScreenToWorldPoint(mouseCurLocation);
    //     Vector3 screenToWorldPointFixed = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
    //     transform.position = Vector3.Lerp(transform.position, screenToWorldPointFixed, 0.5f);


    //     velocity = mouseCurLocation - mousePreviousLocation;//Changes the force to be applied
    //     mousePreviousLocation = mouseCurLocation;
    // }
 
    public void OnMouseUp() {
    //Makes sure there isn't a ludicrous speed
        // Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        // if (rb2D == null){
        //     gameObject.AddComponent(typeof(Rigidbody2D));
        //     rb2D = GetComponent<Rigidbody2D>(); 
        // } 

        // rb2D.bodyType = RigidbodyType2D.Dynamic;
        // rb2D.gravityScale = gravityRatio;
        // Vector2 throwVector = velocity*speedMultiplier;
        // GetComponent<Rigidbody2D>().AddForce(throwVector);
    }

    IEnumerator Shrink(){
        while (transform.localScale.x > scaleThreshold){
            float newScale = transform.localScale.x - shrinkSpeed*Time.deltaTime;
            transform.localScale = new Vector3(newScale, newScale, newScale);
            yield return null;
        }

        Destroy(this.gameObject);
    }


}
