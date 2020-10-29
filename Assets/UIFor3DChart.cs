using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFor3DChart : MonoBehaviour
{
    [SerializeField]
    private ChartArchitect chArchitect;
    private GameObject chArchitectObject;

    private void Awake()
    {

        if (chArchitect == null)
        {
            Debug.LogError("There is no Chart Architect Assigned in object: " + gameObject.name);
        }
        chArchitectObject = chArchitect.gameObject;
        chArchitectObject.SetActive(false);
    }
}
