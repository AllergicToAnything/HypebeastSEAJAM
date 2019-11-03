using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideTriggerer : MonoBehaviour
{
    public GameObject[] objectsToShowWhenEnter;
    public GameObject[] objectsToHideWhenEnter;

    public GameObject[] objectsToShowWhenExit;
    public GameObject[] objectsToHideWhenExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectsToHideWhenEnter != null)
        {
            for (int i = 0; i < objectsToHideWhenEnter.Length; i++)
            {
                objectsToHideWhenEnter[i].SetActive(false);
            }
        }
        if (objectsToShowWhenEnter != null)
        {
            for (int i = 0; i < objectsToShowWhenEnter.Length; i++)
            {
                objectsToShowWhenEnter[i].SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (objectsToHideWhenExit != null)
            {
                for (int i = 0; i < objectsToHideWhenExit.Length; i++)
                {
                    objectsToHideWhenExit[i].SetActive(false);
                }
            }
            if (objectsToShowWhenExit != null)
            {
                Invoke("Show", .8f);

            }
        }
    }
    void Show()
    {
        for (int i = 0; i < objectsToShowWhenExit.Length; i++)
        {
            objectsToShowWhenExit[i].SetActive(true);
        }
    }
}
