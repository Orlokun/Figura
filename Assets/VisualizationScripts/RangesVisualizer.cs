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

    public Circle(int _segments, float _xRadius, float _yRadius)
    {
        segments = _segments;
        xRadius = _xRadius;
        yRadius = _yRadius;
    }
}

public class RangesVisualizer : MonoBehaviour
{
    [SerializeField]
    private float maxRadius;
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
            float rangeRadius = maxRadius / numberOfRanges * (i + 1);
            circles[i] = new Circle(segments, rangeRadius, rangeRadius);
            CircleComponent cComponent = InstantiateCircle(circles[i]);
            cComponent.DrawCircleLine();
            cComponent.transform.SetParent(gameObject.transform);
        }
    }

    public float GetMaxRadius()
    {
        return maxRadius;
    }

    public void SetNumberOfRanges(int _nOfRanges)
    {
        numberOfRanges = _nOfRanges;
    }
     
    public int NumberOfRanges()
    {
        return numberOfRanges;
    }

    void SetDefaultValues()
    {
        numberOfRanges = 5;
        maxRadius = 10;
    }

    CircleComponent InstantiateCircle(Circle _circle)
    {
        GameObject circle = (GameObject)Instantiate(Resources.Load("RangesPrefabs/RangeCircle"));                 //Check hardcoded name
        CircleComponent _cComponent = circle.GetComponent<CircleComponent>();
        _cComponent.SetCircleData(_circle);
        return _cComponent;
    }
}
