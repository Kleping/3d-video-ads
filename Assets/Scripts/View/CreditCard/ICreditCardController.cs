using System.Threading.Tasks;

namespace View.CreditCard
{
    public interface ICreditCardController
    {
        bool ValidateForm(string email, string creditCardNumber, string year, string month);
        Task<bool> ConfirmPurchasing();
    }
}