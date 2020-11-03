using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIClassesState
{
    ActiveClasses,
    ClassesArchive,
    NewClass
}



public class CursosUIController : MonoBehaviour
{
    private Dictionary<string, GameObject> uiProfilePanels;
    [SerializeField]
    GameObject[] profileUIs;

    private UIClassesState classesActualState;

    private void Awake()
    {
        SetInitialUI();
    }

    private void SetInitialUI()
    {
        SetPanelsNameDictionary();
        classesActualState = UIClassesState.ActiveClasses;
        UpdateProfileUIState();
    }


    private void UpdateProfileUIState()
    {
        switch (classesActualState)
        {
            case UIClassesState.ActiveClasses:
                SetObjectActiveFromString("ActiveClassesScroll");
                break;
            case UIClassesState.ClassesArchive:
                SetObjectActiveFromString("ClassesArchiveScroll");
                break;
            case UIClassesState.NewClass:
                SetObjectActiveFromString("NewClassScroll");
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


    public void SetProfileMenu(int _incomingState)
    {
        ClearActiveUIStuff();
        classesActualState = (UIClassesState)_incomingState;
        UpdateProfileUIState();
    }

    private void ClearActiveUIStuff()                           //TODO: Fill cases
    {
        switch (classesActualState)
        {
            case UIClassesState.ActiveClasses:
                break;
            case UIClassesState.ClassesArchive:
                break;
            case UIClassesState.NewClass:
                break;
            default:
                return;
        }
    }
}
