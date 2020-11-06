using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft;
using System.Text;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class LoginScreen : MonoBehaviour
{
    #region GlobalVariables

    #region LoginInputVariables
    [SerializeField]
    GameObject userMailObj;
    [SerializeField]
    GameObject userPassObj;

    [SerializeField]
    TMP_Text userMail;
    [SerializeField]
    TMP_Text userPass;
    #endregion

    string forcedId = "camilo.hernandez@edukativa.cl";
    string forcedPass = "pass123";

    string jLoginData;
    #endregion

    private void Awake()
    {
        CheckBasicSettings();
    }

    void CheckBasicSettings()
    {
        userMail = userMailObj.GetComponent<TMP_Text>();
        userPass = userPassObj.GetComponent<TMP_Text>();
        StaticGameManager.CreateServerInstance();
        StaticGameManager.sMessageHandler.SetApiUrl("https://api.edukativa.cl");
    }

    public void SendLogin()
    {
        CheckUserLoginParameters();
        SendRequestToServer();
    }

    void SendRequestToServer()
    {
        StaticGameManager.sMessageHandler.SendMessageToServer(this,"login", jLoginData, "POST");
    }

    void CheckUserLoginParameters()
    {
        StaticGameManager.pData = ScriptableObject.CreateInstance<PlayerProfileData>();
        StaticGameManager.pData.SetUserLogData(forcedId, forcedPass);     //TODO: Check if Input is Safe. Set Real Data, not Forced
        jLoginData = JsonUtility.ToJson(StaticGameManager.pData.GetLogData());
        Debug.Log(jLoginData);
    }
}

