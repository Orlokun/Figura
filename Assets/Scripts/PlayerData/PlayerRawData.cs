using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLoginStrings", menuName = "PlayerLoginData" )]
public class PlayerRawData : ScriptableObject
{
    public string username;
    public string password;

    public void SetUserPass(string _uName, string _pass)
    {
        username = _uName;
        password = _pass;
    }

}
