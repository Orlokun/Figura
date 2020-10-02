using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    private DimensionsArchitect dArchitect;

    // Start is called before the first frame update
    void Awake()
    {
        dArchitect = FindObjectOfType<DimensionsArchitect>();
    }


    public void AddNewUnitForVisualization()
    {
        dArchitect.AddSatisticsUnit();
    }
}
