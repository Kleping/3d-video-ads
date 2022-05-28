using System.Threading.Tasks;
using Core.Locator;
using Data;
using UnityEngine;

namespace Modules.WebRequest
{
    public interface IWebRequest : IModule
    {
        Task<Response> Get(string endpoint);
        Task<Response> Post(string endpoint, string data);
        Task<Texture2D> GetTexture(string url);
    }
}
