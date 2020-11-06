using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class ServerMessageHandler
{
    [SerializeField]
    static string apiUrl;
    [SerializeField]
    static string token;
    [SerializeField]

    private string lastJsonSent;

    public void SendMessageToServer(MonoBehaviour mb, string _action, string msg, string httpReqType)
    {
        mb.StartCoroutine(Post(_action, msg));
    }

    static IEnumerator Post(string _action, string bodyJsonString)
    {
        var request = new UnityWebRequest(apiUrl + "/" + _action, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.Send();

        if (request.isNetworkError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Successsss");
            
            Dictionary<string, string> dataAnswer = request.GetResponseHeaders();

            Debug.Log("pLogData.Token is: " + request.downloadHandler.data);
            Debug.Log("Status Code: " + request.responseCode);

        }
    }

    public void GetMessageFromServer()
    {

    }

    public void SetApiUrl(string _inApi)
    {
        apiUrl = _inApi;
    }

}
