using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class UnityHttpHelper
{
    private static async Task<string> SendRequest(string requestUrl, string method, string payload = null, string contentType = "application/x-www-form-urlencoded")
    {
        using (UnityWebRequest request = new UnityWebRequest(requestUrl, method))
        {
            if (method == UnityWebRequest.kHttpVerbPOST && payload != null)
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(payload);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.SetRequestHeader("Content-Type", contentType);
            }
            
            request.downloadHandler = new DownloadHandlerBuffer();
            
            try
            {
                await request.SendWebRequest();
                
                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.ProtocolError)
                {
                    throw new Exception(request.error);
                }
                
                return request.downloadHandler.text;
            }
            catch (Exception e)
            {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.LogError($"HTTPRequest Error: {e.Message}");
#endif
                return null;
            }
        }
    }

    public static Task<string> Get(string requestUrl) => SendRequest(requestUrl, UnityWebRequest.kHttpVerbGET);
    public static Task<string> Post(string requestUrl, string payload, string contentType = "application/x-www-form-urlencoded") => SendRequest(requestUrl, UnityWebRequest.kHttpVerbPOST, payload, contentType);
}
