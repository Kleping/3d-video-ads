using System.Collections;
using System.Collections.Generic;
using AdsBanner;
using Core;
using Core.Locator;
using Data;
using Modules.DataStorage;
using Modules.VideoAds;
using UnityEngine;

namespace Modules.AdsRegister
{
    public class AdsRegisterModule : IAdsRegister
    {
        readonly SuitableModuleResolver<IDataStorage> _dataStorage = new SuitableModuleResolver<IDataStorage>();
        readonly SuitableModuleResolver<IVideoAds> _videoAds = new SuitableModuleResolver<IVideoAds>();
        
        readonly List<IAdsBanner> _adBanners = new List<IAdsBanner>();
        
        
        public IEnumerator Init(IApp app)
        {
            _dataStorage.Resolve.OnAdReceived += onAdReceived;
            _videoAds.Resolve.OnAdReceived += onAdReceived;
            yield break;
        }

        public void Link()
        {
        }


        public void CheckIn(IAdsBanner adsBanner)
        {
            _adBanners.Add(adsBanner);
            if (_dataStorage.Resolve.IsEmpty) _videoAds.Resolve.RequestAds();
            else adsBanner.SetInfo(_dataStorage.Resolve.GetRandomAdInfo());
        }

        public bool CheckOut(IAdsBanner adsBanner)
        {
            return _adBanners.Remove(adsBanner);
        }

        void onAdReceived(AdInfo info)
        {
            _adBanners.ForEach(i => i.SetInfo(info));
        }
    }
}