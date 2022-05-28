using System.Collections;
using System.Collections.Generic;
using Core;
using Core.Locator;
using Data;
using Modules.Backend;
using Modules.DataStorage;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.VideoAds
{
    public class VideoAdsModule : IVideoAds
    {
        readonly SuitableModuleResolver<IDataStorage> _dataStorage = new SuitableModuleResolver<IDataStorage>();
        readonly SuitableModuleResolver<IBackend> _backend = new SuitableModuleResolver<IBackend>();
     
        readonly Dictionary<string, AdInfo> _adsByURL = new Dictionary<string, AdInfo>();


        public IEnumerator Init(IApp app)
        {
            yield break;
        }

        public void Link()
        {
        }


        public async void RequestAds()
        {
            var doc = await _backend.Resolve.GetVideoAds();
            if (doc == null) return;
            
            var error = doc.GetElementsByTagName(Constant.ErrorTagName)[0].FirstChild.InnerText;
            if (!string.IsNullOrEmpty(error))
            {
                Debug.LogError(error);
                return;
            }
            
            var mediaFiles = doc.GetElementsByTagName(Constant.MediaFileTagName);
            var ds = _dataStorage.Resolve;
            var urls = new List<string>();
            for (var i = 0; i < mediaFiles.Count; i++)
            {
                var url = mediaFiles[i].FirstChild.InnerText;
                var formattedName = ds.FormatAdName(url);
                var adInfo = new AdInfo(formattedName, url, AdSource.URL);
                if (_adsByURL.ContainsKey(formattedName)) _adsByURL[formattedName] = adInfo;
                else _adsByURL.Add(formattedName, adInfo);
                urls.Add(url);
                OnAdReceived?.Invoke(adInfo);
            }
            
            ds.SaveVideoAds(urls);
        }

        // Ad's identifier, URL path, Ad source which is equals URL
        public event UnityAction<AdInfo> OnAdReceived;
    }
}