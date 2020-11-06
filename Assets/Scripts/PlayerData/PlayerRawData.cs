using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLoginStrings", menuName = "PlayerLoginData" )]
public class PlayerRawData : ScriptableObject
{
    private LogData uLogData;
    private string token; 

    
    public LogData GetLogData()
    {
        return uLogData;
    }

    public void SetToken(string _token)
    {
        token = _token;
    }

    public string GetToken()
    {
        return token;
    }
}
