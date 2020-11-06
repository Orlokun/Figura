using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LogData
{
    public string username;
    public string password;

    public LogData(string _uName, string _pass)
    {
        username = _uName;
        password = _pass;
    }
}

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerData")]
public class PlayerProfileData : ScriptableObject
{
    private LogData lData;
    private string token;
    //General Data
    [SerializeField]
    string uName;
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
            Debug.Log("I got my PSU Data");
            psuPData = (PsuData)PsuData.CreateInstance(typeof(PsuData));
        }
    }

    #endregion
    #region Getters&Setters

    public void SetUserLogData(string _uName, string _pass)
    {
        lData = new LogData(_uName, _pass);
    }

    public LogData GetLogData()
    {
        return lData;
    }




    #endregion
    #region JustForTesting



    #endregion
}
