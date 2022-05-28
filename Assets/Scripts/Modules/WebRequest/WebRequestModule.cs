using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Core;
using Data;
using UnityEngine;
using UnityEngine.Networking;

namespace Modules.WebRequest
{
    public class WebRequestModule : IWebRequest
    {
        public IEnumerator Init(IApp app)
        {
            yield break;
        }

        public void Link()
        {
        }

        public async Task<Response> Get(string endpoint)
        {
            using (var webRequest = UnityWebRequest.Get(endpoint))
            {
                webRequest.SendWebRequest();
                while (!webRequest.isDone) await Task.Yield();
                return processResponse(webRequest);
            }
        }
        
        public async Task<Response> Post(string endpoint, string data)
        {
            using (var webRequest = UnityWebRequest.Post(endpoint, "POST"))
            {
                webRequest.SetRequestHeader("Content-Type", "application/json");
                webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
                webRequest.SendWebRequest();
                while (!webRequest.isDone) await Task.Yield();
                return processResponse(webRequest);
            }
        }

        public async Task<Texture2D> GetTexture(string url)
        {
            using (var webRequest = UnityWebRequestTexture.GetTexture(url))
            {
                webRequest.SendWebRequest();
                while (!webRequest.isDone) await Task.Yield();
                if (webRequest.isHttpError || webRequest.isNetworkError) return null;
                return ((DownloadHandlerTexture) webRequest.downloadHandler).texture;
            }
        }

        
        static Response processResponse(UnityWebRequest webRequest)
        {
            return !webRequest.isHttpError && !webRequest.isNetworkError
                ? new Response(string.Empty, webRequest.downloadHandler)
                : new Response(webRequest.error, null);
        }
    }
}
