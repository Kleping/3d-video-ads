using System.Collections;
using System.Threading.Tasks;
using System.Xml;
using Core;
using Core.Locator;
using Data;
using Modules.WebRequest;
using Newtonsoft.Json;
using UnityEngine;

namespace Modules.Backend
{
    public class BackendModule : IBackend
    {
        readonly SuitableModuleResolver<IWebRequest> _webRequest = new SuitableModuleResolver<IWebRequest>();
        IBackendConfig _config;

        
        public IEnumerator Init(IApp app)
        {
            _config = app.BackendConfig;
            yield break;
        }

        public void Link()
        {
        }
        
        
        public async Task<XmlDocument> GetVideoAds()
        {
            var response = await _webRequest.Resolve.Get(_config.GetEndpoint(EndpointType.GetVideoAds));
            if (!string.IsNullOrEmpty(response.Error)) return null;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(response.Text);
            return xmlDocument;
        }

        public async Task<byte[]> DownloadAd(string url)
        {
            var response = await _webRequest.Resolve.Get(url);
            return string.IsNullOrEmpty(response.Error) ? response.Data : null;
        }

        public async Task<PurchaseInfo> GetPurchaseInfo()
        {
            var endpoint = _config.GetEndpoint(EndpointType.GetPurchaseInfo);
            var response = await _webRequest.Resolve.Post(endpoint, "{}");
            var rawData = JsonConvert.DeserializeAnonymousType(response.Text, new
            {
                title=string.Empty,
                item_id=string.Empty,
                price=string.Empty,
                currency_sign=string.Empty,
                item_image=string.Empty,
            });
            var image = await _webRequest.Resolve.GetTexture(rawData.item_image);
            
            return new PurchaseInfo
                (rawData.item_id, rawData.title, $"{decimal.Parse(rawData.price):f2}", rawData.currency_sign, image);
        }
        
        public async Task<bool> ConfirmPurchase(PurchasingForm purchasingForm)
        {
            var endpoint = _config.GetEndpoint(EndpointType.ConfirmPurchase);
            var body = JsonConvert.SerializeObject(purchasingForm);
            var response = await _webRequest.Resolve.Post(endpoint, body);
            return string.IsNullOrEmpty(response.Error);
        }
    }
}