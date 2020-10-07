using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    LayerMask radarGraphs;
    LayerMask tdGraphs;

    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        Debug.Log("TheCamera Culling mask is: " + camera.cullingMask);
        camera.cullingMask = radarGraphs.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
