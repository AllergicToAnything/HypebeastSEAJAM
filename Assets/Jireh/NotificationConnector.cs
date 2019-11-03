using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationConnector : MonoBehaviour
{
    public LayerMask colliderMask;

    private Camera mainCam;

    private Vector3 screenToWorldPoint;

    private HingeJoint2D hingeJoint;

    private Collider2D collider;

    public NotificationSpawner spawner;

    void Start()
    {
        mainCam = Camera.main;
        hingeJoint = GetComponent<HingeJoint2D>();
    }

    void Update()
    {
        UpdatePointerGraphic();

        if (Input.GetMouseButtonDown(0)){
            RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, 0.1f, Vector3.zero, 0.1f, ~(colliderMask));
            if (hit2D.collider != null){
                spawner.StopAllCoroutines();
                spawner.StartCoroutine(spawner.SpawnNotifications());
                collider = hit2D.collider;
                Vector3 localDif = transform.position - collider.transform.position;
                localDif /= collider.transform.localScale.x;
                AttachPivotToNotification(hit2D.rigidbody, localDif);
                if (hit2D.rigidbody != null)
                    hit2D.rigidbody.bodyType = RigidbodyType2D.Dynamic;
            } else{
                print("nothing found");
            }
        }

        // if (Input.GetMouseButton(0) && collider != null){
        //     Vector3 localDif = transform.position - collider.transform.position;
        //     localDif /= collider.transform.localScale.x;
        //     AdjustPivot(localDif);
        // }

        if (Input.GetMouseButtonUp(0)){
            DetachPivot();
        }
    }

    void UpdatePointerGraphic(){
        screenToWorldPoint = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 screenToWorldPointFixed = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
        transform.position = screenToWorldPointFixed;
    }

    void AttachPivotToNotification(Rigidbody2D target, Vector3 localPos){
        hingeJoint.connectedBody = target;
        hingeJoint.connectedAnchor = localPos;
    }
    
    void DetachPivot(){
        if (hingeJoint.connectedBody != null){
            hingeJoint.connectedBody.transform.parent = null;
        }
        hingeJoint.connectedBody = null;;
        hingeJoint.connectedAnchor = Vector2.zero;
        
    }

    void AdjustPivot(Vector3 localPos){
        hingeJoint.connectedAnchor = localPos;
    }
}
