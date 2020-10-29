using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerData")]
public class PlayerProfileData : ScriptableObject
{
    //General Data
    [SerializeField]
    string name;
    [SerializeField]
    string mail;
    [SerializeField]
    int age;
    [SerializeField]
    string[] skills;

    //ProjectData
    public PsuData psuPData;

    #region Initializers

    public void StartData()
    {
        if (psuPData == null)
        {
            psuPData = (PsuData)PsuData.CreateInstance(typeof(PsuData));
        }
    }

    #endregion
    #region Getters&Setters




    #endregion
    #region JustForTesting



    #endregion
}
