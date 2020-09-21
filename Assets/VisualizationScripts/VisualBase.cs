using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Circle
{
    [Range(0, 50)]
    public int segments;

    [Range(0, 10)]
    public float xRadius;
    [Range(0, 10)]
    public float yRadius;

    public Circle(int _segments, int _xRadius, int _yRadius)
    {
        segments = _segments;
        xRadius = _xRadius;
        yRadius = _yRadius;
    }
}

public class VisualBase : MonoBehaviour
{


    [SerializeField]
    private int maxRadius = 10;
    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 10)]
    private float xRadius;
    [Range(0, 10)]
    private float yRadius;

    public int numberOfRanges;

    public Circle[] circles;


    void Start()
    {
        CheckRanges();
        CreateCircles();
    }

    void CheckRanges()
    {
        bool checkRangeAmount = false;
        bool hasMaxRadius = false;

        if (numberOfRanges == 0)
        {
            Debug.LogError("There is no amount of ranges assigned. Will proceed to Default");
            checkRangeAmount = false;
        }
        else
        {
            checkRangeAmount = true;
        }

        if (maxRadius == 0)
        {
            Debug.LogError("There is no max Radius assigned. Will proceed to Default");
        }
        else
        {
            hasMaxRadius = true;
        }

        if (checkRangeAmount != true || hasMaxRadius != true)
        {
            SetDefaultValues();
        }

    }

    void CreateCircles()
    {
        circles = new Circle[numberOfRanges];

        for (int i = 0; i < circles.Length; i++)
        {
            int rangeRadius = (maxRadius / circles.Length) * (i + 1);
            circles[i] = new Circle(segments, rangeRadius, rangeRadius);
            CircleComponent cComponent = InstantiateCircle(circles[i]);
            cComponent.DrawCircleLine();
        }
    }

    void SetDefaultValues()
    {
        numberOfRanges = 5;
        maxRadius = 10;
    }

    CircleComponent InstantiateCircle(Circle _circle)
    {
        GameObject circle = (GameObject)Instantiate(Resources.Load("RangeCircle"));                 //Check hardcoded name
        CircleComponent _cComponent = circle.GetComponent<CircleComponent>();
        _cComponent.SetCircleData(_circle);
        return _cComponent;
    }
}
