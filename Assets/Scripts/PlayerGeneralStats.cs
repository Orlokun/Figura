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


    void Awake()
    {
        //GetPsuDataFromJsonToo
        psuPData = (PsuData)PsuData.CreateInstance(typeof(PsuData));

    }
    #region Getters&Setters

    


    #endregion
    #region JustForTesting

    



    #endregion  
}
