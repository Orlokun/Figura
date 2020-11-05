using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityGoogleDrive;

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
    string apiAddress = "https://api.edukativa.cl";

    PlayerRawData pLogData;
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
    }

    public void SendLogin()
    {
        CheckUserLoginParameters();
        SendRequestToServer();
    }

    void SendRequestToServer()
    {
        StartCoroutine(PostRequest(apiAddress, "login", jLoginData));
    }

    void CheckUserLoginParameters()
    {
        pLogData = ScriptableObject.CreateInstance<PlayerRawData>();
        pLogData.SetUserPass(userMail.text, userPass.text);     //TODO: Check if Input is Safe
        jLoginData = JsonUtility.ToJson(pLogData);
        Debug.Log(jLoginData);
    }

    IEnumerator PostRequest(string _apiAddress, string verb, string _jSonData)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", pLogData.username);
        form.AddField("password", pLogData.password);

        UnityWebRequest www = UnityWebRequest.Post(_apiAddress + "/" + verb, _jSonData);

        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

    }

    IEnumerator GetRequest(string _apiAddress)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_apiAddress))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = _apiAddress.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
}



/*public void Uploadfile(bool toAppData)
{
    var content = File.ReadAllBytes(uploadPath);
    if (content == null) return;

    var file = new UnityGoogleDrive.Data.File() { Name = Path.GetFileName(uploadPath), Content = content };
    if (toAppData) file.Parents = new List<string> { "appDataFolder" };
    request = GoogleDriveFiles.Create(file);
    request.Fields = new List<string> { "id", "name", "size", "createdTime" };
    request.Send().OnDone += PrintResult;
}

private void PrintResult(UnityGoogleDrive.Data.File file)
{
    result = string.Format("Name: {0} Size: {1:0.00}MB Created: {2:dd.MM.yyyy HH:MM:ss}\nID: {3}",
            file.Name,
            file.Size * .000001f,
            file.CreatedTime,
            file.Id);
    Debug.Log(result);
}
*/


