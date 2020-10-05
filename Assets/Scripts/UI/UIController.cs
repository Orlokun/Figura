using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    private DataUnitsManager dUManager;

    // Start is called before the first frame update
    void Awake()
    {
        dUManager = FindObjectOfType<DataUnitsManager>();
    }


    public void AddNewUnitForVisualization()
    {
        dUManager.AddSatisticsUnit();
    }
}
