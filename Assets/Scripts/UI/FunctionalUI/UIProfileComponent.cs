using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIProfileState
{
    Profile,
    PSUData,
    SkillsData,
    ActiveProjectsData,
    ArchivedProjectsData,
    Personalization,
}

public class UIProfileComponent : MonoBehaviour
{
    private Dictionary<string, GameObject> uiProfilePanels;
    [SerializeField]
    GameObject[] profileUIs;

    private UIProfileState profileActualState;

    [SerializeField]
    ChartArchitect chArchitect;

    private void Awake()
    {
        SetInitialUI();
    }

    private void SetInitialUI()
    {
        SetPanelsNameDictionary();
        chArchitect.gameObject.SetActive(false);            //Make sure there is an object assigned first
        profileActualState = UIProfileState.Profile;
        UpdateProfileUIState();
    }


    private void UpdateProfileUIState()
    {
        switch (profileActualState)
        {
            case UIProfileState.Profile:
                SetObjectActiveFromString("GeneralProfile");
                break;
            case UIProfileState.PSUData:
                SetObjectActiveFromString("PSUCanvas");
                chArchitect.gameObject.SetActive(true);
                chArchitect.InitializeChart(StaticGameManager.GetPlayerData(), DataSetType.psu);
                break;
            default:
                break;
        }
    }


    private void SetPanelsNameDictionary()
    {
        uiProfilePanels = new Dictionary<string, GameObject>();
        for (int i = 0; i < profileUIs.Length; i++)
        {
            string objectName = profileUIs[i].name;
            uiProfilePanels.Add(objectName, profileUIs[i]);
        }
    }


    private void SetObjectActiveFromString(string _key)
    {
        foreach (KeyValuePair<string, GameObject> kvPair in uiProfilePanels)
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


    public void NextProfileMenu(bool next)
    {
        CleanActualProfileStateElements();
        int actualStateInt = (int)profileActualState;
        if (next)
        {
            actualStateInt++;
            if (actualStateInt > 1)
            {
                actualStateInt = 0;
            }
        }
        else
        {
            actualStateInt--;
            if (actualStateInt < 0)
            {
                actualStateInt = 1;     //Hardcoded number. Must be deprecated
            }
        }
        profileActualState = (UIProfileState)actualStateInt;
        UpdateProfileUIState();
    }

    void CleanActualProfileStateElements()
    {
        switch (profileActualState)
        {
            case UIProfileState.Profile:
                break;
            case UIProfileState.PSUData:
                CleanPSUActiveElements();
                break;
            case UIProfileState.SkillsData:
                break;
            case UIProfileState.ActiveProjectsData:
                break;
            case UIProfileState.ArchivedProjectsData:
                break;
            case UIProfileState.Personalization:
                break;
        }
    }

    void CleanPSUActiveElements()
    {
        chArchitect.gameObject.SetActive(false);
    }
}
