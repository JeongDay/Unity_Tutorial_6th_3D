using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AppsScriptManager : MonoBehaviour
{
    public string URL;

    IEnumerator Start()
    {
        string editURL = URL + $"?row={2}&col={2}";
        
        UnityWebRequest www = UnityWebRequest.Get(editURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("네트워크 에러");
            yield break;
        }
        
        string getData = www.downloadHandler.text;
        Debug.Log($"doGet : {getData}");
        
        WWWForm form = new WWWForm();
        form.AddField("value", 1000);
        
        UnityWebRequest www2 = UnityWebRequest.Post(URL, form);
        yield return www2.SendWebRequest();
        
        if (www2.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("네트워크 에러");
            yield break;
        }

        string postData = www2.downloadHandler.text;
        Debug.Log($"doPost : {postData}");
    }
}