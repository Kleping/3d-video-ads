using System.Collections.Generic;
using Core.Locator;
using Data;
using UnityEngine.Events;

namespace Modules.DataStorage
{
    public interface IDataStorage : IModule
    {
        void SaveVideoAds(List<string> urls);
        string FormatAdName(string url);
        AdInfo GetRandomAdInfo();
        
        bool IsEmpty { get; }
        
        event UnityAction<AdInfo> OnAdReceived;
    }
}