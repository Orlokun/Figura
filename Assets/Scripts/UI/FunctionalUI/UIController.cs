using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIGeneralState
{
    Profile,
    ActiveProjects,
    SearchProject,
    RecommendedProjects,
    Community,
    Account,
}

public class UIController : MonoBehaviour
{


    #region GlobalVariables
    
    [SerializeField]
    GameObject[] uiObjects;

    private Dictionary<string, GameObject> uiPanels;

    private UIGeneralState actualState;

    //Many of these things shouldn't be here
    public PlayerManager pManager;

    private ChartArchitect chArchitect;

    private DataUnitsManager dUManager;
    #endregion

    #region Awake Functions
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
        actualState = UIGeneralState.Profile;
        UpdateUIState();
    }

    private void UpdateUIState()
    {
        switch (actualState)
        {
            case UIGeneralState.Profile:
                SetObjectActiveFromString("Perfil");
                break;
            case UIGeneralState.ActiveProjects:
                SetObjectActiveFromString("ProyectosActivos");
                break;
            case UIGeneralState.SearchProject:
                SetObjectActiveFromString("NuevoProyecto");
                break;
            case UIGeneralState.RecommendedProjects:
                SetObjectActiveFromString("ProyectosRecomendados");
                break;
            case UIGeneralState.Community:
                SetObjectActiveFromString("Comunidad");
                break;
            case UIGeneralState.Account:
                SetObjectActiveFromString("Cuenta");
                break;
            default:
                break;
        }
    }

    private void SetPanelsNameDictionary()
    {
        uiPanels = new Dictionary<string, GameObject>();
        for (int i = 0; i < uiObjects.Length; i++)
        {
            string objectName = uiObjects[i].name;
            uiPanels.Add(objectName, uiObjects[i]);
        }
    }

    #endregion

    #region Utilities

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

    public void TurnObjectOn(GameObject _gObject)
    {
        string kName = _gObject.name;
        SetObjectActiveFromString(kName);
    }

    public void SetState(int _state)
    {
        actualState = (UIGeneralState)_state;
        UpdateUIState();
    }

    public void StartChart()
    {
        chArchitect.InitializeData(pManager.pStats, DataSetType.psu);
    }

    private void StartPlayerManager()
    {
        pManager.GetPlayerDataFromFiles();
        chArchitect.playerData = pManager.pStats;
    }

    public void AddNewUnitForVisualization()
    {
        dUManager.AddSatisticsUnit();
    }

    #endregion
}
