using System.Threading.Tasks;
using UnityEngine;

public class Sample : MonoBehaviour
{    
    readonly string[] names = { "Hero", "Heroine", "Thief", "Magician" };
    readonly string[] charaNames = {"ヒーロー", "ヒロイン", "盗賊", "魔法使い" };

    async Task Start()
    {
        // 単なるGetならこれら
        // var yahoo = await HttpClientWrapper.Get("https://www.yahoo.co.jp/");
        // System.IO.File.WriteAllText("yahoo.html", yahoo);

        // var google = await UnityHttpHelper.Get("https://www.google.co.jp/");
        // System.IO.File.WriteAllText("google.html", google);

        int index = Random.Range(0, names.Length);

        // キャラクターのインスタンスを作成
        Character character = new Character(names[index], charaNames[index], Random.Range(100,1000), Random.Range(10.0f, 100.0f));
        Debug.Log("character: " + character.ToString());
        
        // JSON にシリアライズ
        string jsonPayload = character.ToJson();
        Debug.Log("jsonPayload: " + jsonPayload);

        // 送信先URL
        string url = "";
#if UNITY_EDITOR
        url = "";
#endif
        
        // HttpClientWrapper を使って POST 送信
        string response = await HttpClientWrapper.Post(url, jsonPayload, "application/json");
        
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
