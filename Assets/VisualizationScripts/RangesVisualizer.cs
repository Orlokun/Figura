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
    public RadarGraphData rData;

    [Range(0, 50)]
    public int segments = 50;

    public Circle[] circles;

    void Awake()
    {
        CheckRanges();
        CreateCircles();
    }

    void CheckRanges()
    {

    }

    void CreateCircles()
    {
        GameObject circlesHolder = (GameObject)Instantiate(Resources.Load("RangesPrefabs/RangesHolder"), transform, false);
        circlesHolder.transform.localPosition = new Vector3(0,0,0);
        circles = new Circle[rData.GetNumberOfRanges()];
        for (int i = 0; i < circles.Length; i++)
        {
            float rangeRadius = rData.GetMaxRadius() / rData.GetNumberOfRanges() * (i + 1);
            circles[i] = new Circle(segments, rangeRadius, rangeRadius);
            CircleComponent cComponent = InstantiateCircle(circles[i],circlesHolder.transform);
            cComponent.DrawCircleLine();
        }
    }

    CircleComponent InstantiateCircle(Circle _circle, Transform cTransform)
    {
        GameObject circle = (GameObject)Instantiate(Resources.Load("RangesPrefabs/RangeCircle"), cTransform, false);                 //Check hardcoded name
        circle.transform.localPosition = new Vector3(0, 0, 0);
        CircleComponent _cComponent = circle.GetComponent<CircleComponent>();
        _cComponent.SetCircleData(_circle);
        return _cComponent;
    }
}
