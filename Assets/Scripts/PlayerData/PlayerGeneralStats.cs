using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

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

[SerializeField]
public class User
{
    public string name { get; set; }
    public int rol { get; set; }
    public string id { get; set; }
}

[SerializeField]
public class ReceivedUserData
{
    public bool success { get; set; }
    public User user { get; set; }
}

public class PlayerProfileData
{
    private LogData lData;
    static ReceivedUserData myUser;
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

    public void ReceiveUniqueData(string _msg)
    {
        Debug.Log("THIS IS MY MESSAGE: " + _msg);

        ReceivedUserData userData = JsonConvert.DeserializeObject<ReceivedUserData>(_msg);
        ReceivedUserData uData = userData;
    }

    public void SetUniqueData(string msg)
    {
        ReceiveUniqueData(msg);
    }


    #region JustForTesting



    #endregion
}
