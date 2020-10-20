using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIGeneralState
{
    GenearalProfile,
    Statistics,
    SearchProject,
    ActiveProjects,
    Account
}

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameObject[] profileCanvases;

    private Dictionary<string, GameObject> uiPanels;

    private UIGeneralState actualState;

    public PlayerManager pManager;

    private ChartArchitect chArchitect;

    private DataUnitsManager dUManager;

    // Start is called before the first frame update
    void Awake()
    {
        dUManager = FindObjectOfType<DataUnitsManager>();
        chArchitect = FindObjectOfType<ChartArchitect>();
        pManager = (PlayerManager)PlayerManager.CreateInstance(typeof(PlayerManager));
        StartPlayerManager();

        SetInitialUI();

        //Move away from here
        StartChart();
    }
    

    private void SetInitialUI()
    {
        SetPanelsNameDictionary();
        actualState = UIGeneralState.GenearalProfile;
        UpdateUIState();
    }

    private void UpdateUIState()
    {
        switch (actualState)
        {
            case UIGeneralState.GenearalProfile:
                SetObjectActiveFromString("GeneralProfile");
                break;
            case UIGeneralState.Statistics:
                SetObjectActiveFromString("PSUCanvas");
                break;
            default:
                break;
        }
    }

    public void NextUIMenu(bool next)
    {
        int actualStateInt = (int)actualState;
        if (next)
        {
            actualStateInt++;
            if (actualStateInt >1)
            {
                actualStateInt = 0;
            }
        }
        else
        {
            actualStateInt--;
            if (actualStateInt<0)
            {
                actualStateInt = 1;     //Hardcoded number. Must be deprecated
            }
        }
        actualState = (UIGeneralState)actualStateInt;
        UpdateUIState();
    }


    private void SetObjectActiveFromString(string _key)
    {
        foreach (KeyValuePair<string, GameObject> kvPair in uiPanels)
        {
            if (kvPair.Key == _key)
            {
                kvPair.Value.SetActive(true);
            }
            else
            {
                kvPair.Value.SetActive(false);
            }
        }
    }

    private void SetPanelsNameDictionary()
    {
        uiPanels = new Dictionary<string, GameObject>();
        for (int i = 0; i<profileCanvases.Length; i++)
        {
            string objectName = profileCanvases[i].name;
            uiPanels.Add(objectName, profileCanvases[i]);
        }
    }

    void TurnObjectOn(GameObject _gObject)
    {

    }

    public void StartChart()
    {
        chArchitect.InitializeData(pManager.pStats, DataSetType.psu);
    }


    private void StartPlayerManager()
    {
        pManager.GetPlayerDataFromFiles();
    }
    public void AddNewUnitForVisualization()
    {
        dUManager.AddSatisticsUnit();
    }
}
