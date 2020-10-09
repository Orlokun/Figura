using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleGraph : MonoBehaviour
{
    //Chart myChart = FindObjectOfType<Chart>();

    private Vector3 initialPosition;
    private int zGridPosition;
    private int xGridPosition;
    

    private Vector3 myGridPosition;
    private Color myColor;

    private float yScore;
    private float yTopPosition;

    private float maxYPosition = 10f;
    private float speed = 6;

    int waitTime = 100;
    int actualTime;

    private void Start()
    {

    }

    private void Update()
    {
        actualTime++;
        if (actualTime>= waitTime)
        {
            if (transform.localScale.y < maxYPosition)
            {
                float upY = speed * Time.deltaTime;
                transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y + upY, transform.localScale.z);
                if(transform.localScale.y > maxYPosition)
                {
                    transform.localScale = new Vector3(transform.localScale.x, maxYPosition, transform.localScale.z);
                }
                transform.position = new Vector3(transform.position.x, transform.position.y + upY, transform.position.z);
            }
        }
    }
}
