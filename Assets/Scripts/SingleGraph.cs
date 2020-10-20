using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SingleGraphMovState
{
    increasing,
    decreasing,
    paused,
}

public class SingleGraph : MonoBehaviour
{
    //Chart myChart = FindObjectOfType<Chart>();

    private SingleGraphMovState movState;

    private Color myColor;

    private float yScore;
    private float yTopPosition;

    private float maxYPosition = 5f;
    private float speed = 6;

    int waitTime = 100;
    int actualTime;


    private void Awake()
    {
        
    }

    private void Update()
    {
        switch (movState)
        {
            case SingleGraphMovState.decreasing:
                break;
            case SingleGraphMovState.increasing:
                break;
            case SingleGraphMovState.paused:
                break;
            default:
                break;
        }

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
