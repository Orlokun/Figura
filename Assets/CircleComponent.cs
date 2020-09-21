using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class CircleComponent : MonoBehaviour
{
    int segments;
    float rangeRadius;

    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        SetTransformDefaultState();
    }


    public void SetCircleData(Circle _cComponent)
    {
        segments = _cComponent.segments;
        rangeRadius = _cComponent.xRadius;

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
    }

    public void DrawCircleLine()
    {
        float x;
        float y;
        float z = 0;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * rangeRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * rangeRadius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }

        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.startColor = new Color(176, 9, 9);
        line.endColor = new Color(176, 9, 9);
    }

    private void SetTransformDefaultState()
    {
        //transform.position = new Vector3(0,1f,0);
        transform.Rotate(90f, 0f, 0f);
    }
}