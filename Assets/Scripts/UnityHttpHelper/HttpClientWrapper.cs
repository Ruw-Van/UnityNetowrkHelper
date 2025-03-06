using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class HttpClientWrapper
{
    private static readonly HttpClient _httpClient = new HttpClient();

    private static async Task<string> SendRequest(string requestUrl, HttpMethod method, string payload = null)
    {
        using (var request = new HttpRequestMessage(method, requestUrl))
        {
            if (method == HttpMethod.Post && payload != null)
            {
                request.Content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
            }

            try
            {
                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException e)
            {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.LogError($"HttpClient Error: {e.Message}");
#endif
                return null;
            }
        }
    }

    public static Task<string> Get(string requestUrl) => SendRequest(requestUrl, HttpMethod.Get);
    public static Task<string> Post(string requestUrl, string payload) => SendRequest(requestUrl, HttpMethod.Post, payload);
}
