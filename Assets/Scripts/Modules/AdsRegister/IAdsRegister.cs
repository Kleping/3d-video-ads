using AdsBanner;
using Core.Locator;

namespace Modules.AdsRegister
{
    public interface IAdsRegister : IModule
    {
        void CheckIn(IAdsBanner adsBanner);
        bool CheckOut(IAdsBanner adsBanner);
    }
}