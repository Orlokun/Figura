using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorComponent : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, .3f, 0,Space.Self);
    }
}
