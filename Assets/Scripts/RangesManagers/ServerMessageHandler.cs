using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class PlayerAuthData
{
    public int code;
    public string expire;
    public string token;

    public string MyToken()
    {
        return token;
    }
}

public class ServerMessageHandler
{
    #region ServerGlobalVariables
    [SerializeField]
    static string apiUrl;

    [SerializeField]
    static PlayerAuthData pAuthData;


    #endregion  


    public void SendMessageToServer(MonoBehaviour mb, string _action, string msg, string httpReqType)
    {
        mb.StartCoroutine(Post(_action, msg));
    }

    public void SetpAuthData(PlayerAuthData _pAuthData)
    {

    }

    public void SetApiUrl(string _inApi)
    {
        apiUrl = _inApi;
    }

    static IEnumerator Post(string _action, string bodyJsonString)
    {
        var request = HandlePostRequest(_action);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.LogError("Error: " + request.error);
        }

        else
        {
            HandleReceivedMessage(_action, request.downloadHandler.text);
        }
    }

    static UnityWebRequest HandlePostRequest(string _action)
    {
        if (_action == "login")
        {
            return new UnityWebRequest(apiUrl + "/" + _action, "POST");
        }
        else
        {
            UnityWebRequest request = new UnityWebRequest(apiUrl + "/" + _action, "POST");
            request.SetRequestHeader("Authorization", "Bearer " + pAuthData.MyToken());
            return request;
        }
    }

    #region ReceivedMessagesHandler

    static void HandleReceivedMessage(string _action, string incomingJsonMessage)
    {

        switch (_action)
        {
            case "login":
                HandleLoginMessage(incomingJsonMessage);
                break;

            default:
                return;
        }
    }

    static void HandleLoginMessage(string msg)
    {
        pAuthData = new PlayerAuthData();
        JsonUtility.FromJsonOverwrite(msg, pAuthData);
        Debug.Log("pAuthData Ready. token = " + pAuthData.MyToken());
    }

    #endregion
}
