using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeGenerator : MonoBehaviour
{
    private float fieldOfView;
    private float viewRadius;

    public Vector3 DirectionFromAngle(float angleInDegrees)
    {
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
