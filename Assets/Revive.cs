using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Revive : MonoBehaviour
{
    public GameObject objectToFollow;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow==null)
        {
            // GameObject obj = Instantiate(objectToFollow);
            // GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = obj.transform;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
