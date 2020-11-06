using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIGeneralState
{
    Profile,
    Cursos,
    ActiveProjects,
    SearchProjects,
    Calendar,
    Community,
}

public class UIController : MonoBehaviour
{
    #region GlobalVariables
    [SerializeField]
    GameObject[] uiObjects;

    private Dictionary<string, GameObject> uiPanels;
    private UIGeneralState actualState;

    #endregion

    #region Awake Functions
    void Awake()
    {
        GetPlayerProfileData();             //TODO: This is for initializing. Should be somewhere else
        SetInitialUI();
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
                SetObjectActiveFromString("ProfileUI");
                break;
            case UIGeneralState.Cursos:
                SetObjectActiveFromString("CursosUI");
                break;
            case UIGeneralState.ActiveProjects:
                SetObjectActiveFromString("ActiveProjectUI");
                break;
            case UIGeneralState.SearchProjects:
                SetObjectActiveFromString("SearchProjectsUI");
                break;
            case UIGeneralState.Calendar:
                SetObjectActiveFromString("CalendarUI");
                break;
            case UIGeneralState.Community:
                SetObjectActiveFromString("CommunityUI");
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

    private void GetPlayerProfileData()             //TODO: This should not be here!  Move Server initialization & PData Creation
    {
        StaticGameManager.CreatePlayerDataIfNeeded();
    }

    #endregion
}
