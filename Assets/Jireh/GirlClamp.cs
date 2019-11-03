using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GirlClamp : MonoBehaviour
{
    private float yClamp;
    public PlayerController playerScript;
    public float startSequenceLength = 7;
    public float girlTransformDelay = 2.5f;
    public float glassCrackDelay = 1.5f;
    public float endPointX;
    private Animator animator;
    public Animator glassCrackAnimation;
    public bool endAnimationPlayed = false;
    void Start()
    {
        yClamp = transform.position.y;
        playerScript.enabled = false;
        animator = GetComponent<Animator>();
        StartCoroutine(StartSequence());
        glassCrackAnimation.gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, yClamp, transform.position.z);
        if (playerScript.transform.position.x > endPointX && !endAnimationPlayed){
            endAnimationPlayed = true;
            StartCoroutine(EndSequence());
        }
    }

    IEnumerator StartSequence(){
        yield return new WaitForSeconds(startSequenceLength);
        playerScript.enabled = true;
    }

    IEnumerator EndSequence(){
        playerScript.enabled = false;
        animator.SetBool("isFinished", true);
        yield return new WaitForSeconds(girlTransformDelay);
        glassCrackAnimation.gameObject.SetActive(true);
        yield return new WaitForSeconds(glassCrackDelay);
        glassCrackAnimation.SetBool("isFinished", true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Scene - Level 2");
    }


    
}
