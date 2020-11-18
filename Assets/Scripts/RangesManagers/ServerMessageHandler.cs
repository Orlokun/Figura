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

    [SerializeField]
    MonoBehaviour myMb;

    #endregion

    #region SendRequestUtilsAndOverloads
    public void SendMessageToServer(MonoBehaviour mb, string _action, string msg, string httpReqType)
    {
        if (myMb != null && myMb.isActiveAndEnabled)
        {
            StartRoutineMessaging(httpReqType, _action, msg);
            
        }
        else
        {
            myMb = mb;
            StartRoutineMessaging(httpReqType, _action, msg);
        }
    }

    public void SendMessageToServer(string _action, string msg, string httpReqType)
    {
        if (myMb != null && myMb.isActiveAndEnabled)
        {
            StartRoutineMessaging(httpReqType, _action, msg);
        }
        else
        {
            //TODO: Secure the MB Instance
            StartRoutineMessaging(httpReqType, _action, msg);
        }
    }


    public void SendMessageToServerWithoutJSon(string _action, string httpReqType)
    {
        if (myMb != null && myMb.isActiveAndEnabled)
        {
            StartRoutineMessaging(httpReqType, _action);
        }
        else
        {
            //TODO: Secure the MB Instance
            StartRoutineMessaging(httpReqType, _action);
        }
    }

    public void StartRoutineMessaging(string reqType, string _act, string _msg)
    {
        switch(reqType)
        {
            case "POST":
                myMb.StartCoroutine(Post(_act, _msg));
                break;
            case "GET":
                break;
            //TODO: Build function for other reqTypes
            default:
                break;
        }
    }

    public void StartRoutineMessaging(string reqType, string _act)
    {
        switch (reqType)
        {
            case "POST":
                myMb.StartCoroutine(Post(_act));
                break;
            case "GET":
                myMb.StartCoroutine(Get(_act));
                break;
            //TODO: Build function for other reqTypes
            default:
                break;
        }
    }

    public void SetApiUrl(string _inApi)
    {
        apiUrl = _inApi;
    }

    #region POSTHANDLERS
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

    static IEnumerator Post(string _action)
    {
        var request = HandlePostRequest(_action);
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
    #endregion

    #region GETHANDLERS

    static IEnumerator Get(string _action)
    {
        var request = HandleGetRequest(_action);
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

    static UnityWebRequest HandleGetRequest(string _action)
    {
        if (_action == "login")
        {
            return new UnityWebRequest(apiUrl + "/" + _action, "GET");
        }
        else
        {
            UnityWebRequest request = new UnityWebRequest(apiUrl + "/" + _action, "GET");
            request.SetRequestHeader("Authorization", "Bearer " + pAuthData.MyToken());
            return request;
        }
    }

    #endregion

    #region ReceivedMessagesHandler
    static void HandleReceivedMessage(string _action, string incomingJsonMessage)
    {
        switch (_action)
        {
            case "login":
                HandleLoginMessageAnswer(incomingJsonMessage);
                break;
            case "users/me":
                HandleGetUserMessageAnswer(incomingJsonMessage);
                break;
            default:
                return;
        }
    }

    static void HandleLoginMessageAnswer(string msg)
    {
        pAuthData = new PlayerAuthData();
        JsonUtility.FromJsonOverwrite(msg, pAuthData);

        Debug.Log(pAuthData.code + " / ");
        StaticGameManager.sMessageHandler.SendMessageToServerWithoutJSon("users/me", "GET");
    }

    static void HandleGetUserMessageAnswer(string msg)
    {
        StaticGameManager.pData.SetUniqueData(msg);
    }


    #endregion

    #endregion
}