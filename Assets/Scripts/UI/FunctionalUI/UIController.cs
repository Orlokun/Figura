using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public PlayerManager pManager; 

    private DataUnitsManager dUManager;
    ChartArchitect chArchitect;

    // Start is called before the first frame update
    void Awake()
    {
        dUManager = FindObjectOfType<DataUnitsManager>();
        pManager = (PlayerManager)PlayerManager.CreateInstance(typeof(PlayerManager));
        chArchitect = FindObjectOfType<ChartArchitect>();
        StartData();

        //Move away fro here
        StartChart();
    }

    public void StartChart()
    {
        chArchitect.InitializeData(pManager.pStats, DataSetType.psu);
    }


    private void StartData()
    {
        pManager.GetPlayerDataFromFiles();
    }
    public void AddNewUnitForVisualization()
    {
        dUManager.AddSatisticsUnit();
    }
}
