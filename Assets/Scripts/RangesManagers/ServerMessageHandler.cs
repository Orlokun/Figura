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

public struct MeReq
{
    public string id;

    public MeReq(string _id)
    {
        id = _id;
    }
}

public class ServerMessageHandler
{
    #region ServerGlobalVariables
    [SerializeField]
    static string apiUrl;
    [SerializeField]
    static MeReq meReq;

    [SerializeField]
    static PlayerAuthData pAuthData;

    [SerializeField]
    MonoBehaviour myMb;

    #endregion

    #region SendRequestUtils
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
    #endregion

    #region ReceivedMessagesHandler

    static void HandleReceivedMessage(string _action, string incomingJsonMessage)
    {
        switch (_action)
        {
            case "login":
                HandleLoginMessageAnswer(incomingJsonMessage);
                break;
            case "users":
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

        meReq = new MeReq("me");
        string jsonId = JsonUtility.ToJson(meReq);
        StaticGameManager.sMessageHandler.SendMessageToServer("users", jsonId, "POST");
    }

    static void HandleGetUserMessageAnswer(string msg)
    {

    }


    #endregion
}