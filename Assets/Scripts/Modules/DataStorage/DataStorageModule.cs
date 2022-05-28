using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core;
using Core.Locator;
using Data;
using Modules.Backend;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Modules.DataStorage
{
    public class DataStorageModule : IDataStorage
    {
        readonly SuitableModuleResolver<IBackend> _backend = new SuitableModuleResolver<IBackend>();
        readonly Dictionary<string, AdInfo> _adsInfoByFile = new Dictionary<string, AdInfo>();


        public IEnumerator Init(IApp app)
        {
            var videoFormats = new[] 
                {".asf", ".avi", ".dv", ".m4v", ".mov", ".mp4", ".mpg", ".mpeg", ".ogv", ".vp8", ".webm", ".wmv"};
            
            foreach (var file in Directory.GetFiles(Application.persistentDataPath))
            {
                if (!videoFormats.Contains(Path.GetExtension(file))) continue;
                var formattedName = Path.GetFileName(file);
                _adsInfoByFile.Add(formattedName, new AdInfo(formattedName, file, AdSource.File));
            }

            yield break;
        }

        public void Link()
        {
        }

        
        public async void SaveVideoAds(List<string> urls)
        {
            foreach (var url in urls)
            {
                var data = await _backend.Resolve.DownloadAd(url);
                var formattedName = FormatAdName(url);
                var path = formPath(formattedName);
                File.WriteAllBytes(path, data);
                var adInfo = new AdInfo(formattedName, path, AdSource.File);
                if (_adsInfoByFile.ContainsKey(formattedName)) _adsInfoByFile[formattedName] = adInfo;
                else _adsInfoByFile.Add(formattedName, adInfo);
                OnAdReceived?.Invoke(adInfo);
            }
        }
        
        public string FormatAdName(string url)
        {
            var formattedName = url.Split(new[] {Constant.ProtocolTag}, StringSplitOptions.RemoveEmptyEntries)[1];
            return formattedName.Replace('/', '.');
        }

        public AdInfo GetRandomAdInfo()
        {
            return _adsInfoByFile.Count != 0 
                ? _adsInfoByFile.ElementAt(Random.Range(0, _adsInfoByFile.Count - 1)).Value 
                : null;
        }
        
        static string formPath(string formattedName)
        {
            return $"{Application.persistentDataPath}/{formattedName}";
        }
        
        
        public bool IsEmpty => _adsInfoByFile.Count == 0;

        public event UnityAction<AdInfo> OnAdReceived;
    }
}