using System.Threading.Tasks;
using Data;

namespace View.Common
{
    public interface ICommonController
    {
        Task<PurchaseInfo> InitiatePurchasing();
    }
}