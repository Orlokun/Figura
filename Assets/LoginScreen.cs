using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityGoogleDrive;

public class LoginScreen : MonoBehaviour
{
    [SerializeField]
    string uploadPath;

    static string appID = "081b9ff2";
    static string appKeys = "bac6732b71e50ed8ca9ee6180cbbee91";



    /*public int ResultsPerPage = 100;
    private string query = string.Empty;*/
    private string fileId = string.Empty;
    private Dictionary<string, string> results;

    private GoogleDriveFiles.GetRequest request;
    private string result;

    private void Awake()
    {
        uploadPath = "https://od-api.oxforddictionaries.com/api/v2/entries/ES/<word_id>";
    }

    public void MakeOxfordrequest()
    {
        
    }





    public void GetFile()
    {
        request = GoogleDriveFiles.Get(fileId);
        request.Fields = new List<string> { "name, size, createdTime" };
        request.Send().OnDone += BuildResultString;
    }

    private void BuildResultString(UnityGoogleDrive.Data.File file)
    {
        result = string.Format("Name: {0} Size: {1:0.00}MB Created: {2:dd.MM.yyyy HH:MM:ss}",
                file.Name,
                file.Size * .000001f,
                file.CreatedTime);

        Debug.Log(result);
    }


    /*
    public void ListFiles(string nextPageToken = null)
    {
        request = GoogleDriveFiles.List();
        request.Fields = new List<string> { "nextPageToken, files(id, name, size, createdTime)" };
        request.PageSize = ResultsPerPage;
        if (!string.IsNullOrEmpty(query))
            request.Q = string.Format("name contains '{0}'", query);
        if (!string.IsNullOrEmpty(nextPageToken))
            request.PageToken = nextPageToken;
        request.Send().OnDone += BuildResults;
    }

    private void BuildResults(UnityGoogleDrive.Data.FileList fileList)
    {
        results = new Dictionary<string, string>();

        foreach (var file in fileList.Files)
        {
            var fileInfo = string.Format("Name: {0} Size: {1:0.00}MB Created: {2:dd.MM.yyyy}",
                file.Name,
                file.Size * .000001f,
                file.CreatedTime);
            results.Add(file.Id, fileInfo);
        }
        Debug.Log(result);
    }

    private bool NextPageExists()
    {
        return request != null &&
            request.ResponseData != null &&
            !string.IsNullOrEmpty(request.ResponseData.NextPageToken);
    }
    */
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


