using System.Threading.Tasks;
using Core.Locator;
using Data;
using Modules.Backend;

namespace View.CreditCard
{
    public class CreditCardController : ICreditCardController
    {
        readonly SuitableModuleResolver<IBackend> _backend = new SuitableModuleResolver<IBackend>();
        readonly PurchasingForm _form = new PurchasingForm();

        
        public bool ValidateForm(string email, string creditCardNumber, string year, string month)
        {
            if (!validateEmail(email)) return false;
            if (creditCardNumber.Length != 16) return false;
            if (year.Length != 4) return false;
            if (string.IsNullOrEmpty(month)) return false;
            var intMonth = int.Parse(month);
            if (intMonth < 1 || intMonth > 12) return false;
            _form.Email = email;
            _form.CreditCardNumber = creditCardNumber;
            _form.Year = year;
            _form.Month = month;
            return true;
        }
        
        public async Task<bool> ConfirmPurchasing()
        {
            return await _backend.Resolve.ConfirmPurchase(_form);
        }
        
        
        static bool validateEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith(".")) {
                return false; // suggested by @TK-421
            }
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch {
                return false;
            }
        }
    }
}