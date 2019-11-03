using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBg : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothingRate = 1;

    private Transform mainCam;
    private Vector3 previousCameraPos;

    void Start()
    {
        mainCam = Camera.main.transform;
        previousCameraPos = mainCam.position;

        parallaxScales = new float[backgrounds.Length];
        for (int i=0; i< backgrounds.Length;i++){
            parallaxScales[i] = backgrounds[i].position.z*-1;
        }
    }

    void Update()
    {
        for (int i=0; i< backgrounds.Length;i++){
            parallaxScales[i] = backgrounds[i].position.z*-1;
            float parallax = (previousCameraPos.x - mainCam.position.x)*parallaxScales[i];

            float targetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(targetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothingRate*Time.deltaTime);
        }

        previousCameraPos = mainCam.position;
    }
}
