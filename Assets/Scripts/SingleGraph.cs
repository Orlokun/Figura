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
    private SingleGraphMovState movState;
    private MeshRenderer mRenderer;
    Material myMat;

    //Musnt be public. Must be deprecated
    public int maxScore;
    public int actualScore;
    public string testName;
    public int testNumber;

    /***********************************/

    private ChartArchitect chArchitect;
    private Color myColor;

    private float maxYPosition = 5f;
    private float speed = 6;

    float cSize;
    int idleTime;

    private void Awake()
    {
        chArchitect = FindObjectOfType<ChartArchitect>();
        myMat = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        switch (movState)
        {
            case SingleGraphMovState.decreasing:
                Decrease();
                break;
            case SingleGraphMovState.increasing:
                Increase();
                break;
            case SingleGraphMovState.paused:
                break;
            default:
                break;
        }
    }

    private void Increase()
    {
        cSize = transform.localScale.y;
        if (transform.localScale.y < maxYPosition * ((float)actualScore / maxScore))
        {
            float upY = speed * Time.deltaTime;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + upY, transform.localScale.z);
            if (transform.localScale.y > maxYPosition)
            {
                transform.localScale = new Vector3(transform.localScale.x, maxYPosition, transform.localScale.z);
            }
            transform.position = new Vector3(transform.position.x, transform.position.y + upY, transform.position.z);
        }

        //TestMovement
        if(cSize == transform.localScale.y)
        {
            idleTime++;
            if (idleTime == 100)
            {
                idleTime = 0;
                movState = SingleGraphMovState.decreasing;
            }
        }
        UpdateColor();
    }

    private void Decrease()
    {
        cSize = transform.localScale.y;
        if (transform.localScale.y > 0f)
        {
            float downY = speed * Time.deltaTime;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - downY, transform.localScale.z);
            if (transform.localScale.y < 0f)
            {
                transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
            }
            transform.position = new Vector3(transform.position.x, transform.position.y - downY, transform.position.z);
            if (transform.position.y < 0.1f)
            {
                transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
            }
        }

        //TestMovement
        if (cSize == transform.localScale.y)
        {
            idleTime++;
            if (idleTime == 100)
            {
                idleTime = 0;
                movState = SingleGraphMovState.increasing;
            }
        }
        UpdateColor();

    }

    private void UpdateColor()
    {
        myMat.color = chArchitect.gradient.Evaluate(transform.localScale.y / maxYPosition);
    }

    public void SetGraphState(SingleGraphMovState _gState)
    {
        movState = _gState;
    }
}
