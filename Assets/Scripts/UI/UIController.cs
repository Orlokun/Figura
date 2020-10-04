using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    private DimensionsArchitect dArchitect;
    private DataUnitsManager dUManager;

    // Start is called before the first frame update
    void Awake()
    {
        dArchitect = FindObjectOfType<DimensionsArchitect>();
        dUManager = FindObjectOfType<DataUnitsManager>();
    }


    public void AddNewUnitForVisualization()
    {
        dUManager.AddSatisticsUnit();
    }
}
