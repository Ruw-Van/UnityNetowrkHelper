using System.Threading.Tasks;
using UnityEngine;

public class Sample : MonoBehaviour
{
    async Task Start()
    {
        // 単なるGetならこれら
        // var yahoo = await HttpClientWrapper.Get("https://www.yahoo.co.jp/");
        // System.IO.File.WriteAllText("yahoo.html", yahoo);

        // var google = await UnityHttpHelper.Get("https://www.google.co.jp/");
        // System.IO.File.WriteAllText("google.html", google);

        // キャラクターのインスタンスを作成
        Character character = new Character("Hero", "勇者", 100, 50);
        Debug.Log("character: " + character.ToString());
        
        // JSON にシリアライズ
        string jsonPayload = character.ToJson();
        Debug.Log("jsonPayload: " + jsonPayload);

        // 送信先URL
        string url = "https://u.amatukami.com/~fport/_r.php";
        
        // HttpClientWrapper を使って POST 送信
        string response = await HttpClientWrapper.Post(url, jsonPayload);
        
        // レスポンスをログに表示
        if (response != null)
        {
            Debug.Log("Response: " + response);
            System.IO.File.WriteAllText("HttpClientWrapper.html", response);
        }
        else
        {
            Debug.LogError("Failed to send JSON.");
        }
#if UNITY_EDITOR
    	UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
