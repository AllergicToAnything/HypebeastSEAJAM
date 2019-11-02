using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public float gravity = -9.81f;
    public float speedMultiplier = 10;

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
        if (GetComponent<Rigidbody2D>() != null){
            gameObject.transform.right = GetComponent<Rigidbody2D>().velocity;
        }
        
    }

     void OnMouseDown() {
        //This grabs the position of the object in the world and turns it into the position on the screen
        print("hit");
        //Sets the mouse pointers vector3
        mousePreviousLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    void OnMouseDrag() {
        Vector3 mouseCurLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 screenToWorldPoint = mainCam.ScreenToWorldPoint(mouseCurLocation);
        Vector3 screenToWorldPointFixed = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
        transform.position = Vector3.Lerp(transform.position, screenToWorldPointFixed, 0.5f);


        velocity = mouseCurLocation - mousePreviousLocation;//Changes the force to be applied
        mousePreviousLocation = mouseCurLocation;
    }
 
    public void OnMouseUp() {
    //Makes sure there isn't a ludicrous speed
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null){
            gameObject.AddComponent(typeof(Rigidbody2D));
            rb2D = GetComponent<Rigidbody2D>(); 
        } 

        rb2D.bodyType = RigidbodyType2D.Dynamic;
        
        GetComponent<Rigidbody2D>().gravityScale = 1;
        Vector2 throwVector = velocity*speedMultiplier;
        Vector2 gravityVector = new Vector2(0, gravity);
        GetComponent<Rigidbody2D>().AddForce(throwVector + gravityVector);
        
    }
}
