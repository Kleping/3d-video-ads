using System.Threading.Tasks;
using System.Xml;
using Core.Locator;
using Data;

namespace Modules.Backend
{
    public interface IBackend : IModule
    {
        Task<XmlDocument> GetVideoAds();
        Task<byte[]> DownloadAd(string url);
        Task<PurchaseInfo> GetPurchaseInfo();
        Task<bool> ConfirmPurchase(PurchasingForm purchasingForm);
    }
}