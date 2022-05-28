using System.Threading.Tasks;
using Core.Locator;
using Data;
using Modules.Backend;

namespace View.Common
{
    public class CommonController : ICommonController
    {
        readonly SuitableModuleResolver<IBackend> _backend = new SuitableModuleResolver<IBackend>();
        
        public async Task<PurchaseInfo> InitiatePurchasing()
        {
            return await _backend.Resolve.GetPurchaseInfo();
        }
    }
}
