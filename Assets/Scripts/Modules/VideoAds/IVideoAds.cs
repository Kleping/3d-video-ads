using Core.Locator;
using Data;
using UnityEngine.Events;

namespace Modules.VideoAds
{
    public interface IVideoAds : IModule
    {
        void RequestAds();
        
        event UnityAction<AdInfo> OnAdReceived;
    }
}