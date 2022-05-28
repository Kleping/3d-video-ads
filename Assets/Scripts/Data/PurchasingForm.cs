using System;
using Newtonsoft.Json;

namespace Data
{
    [Serializable]
    public class PurchasingForm
    {
        [JsonProperty("email")]
        public string Email;

        [JsonProperty("credit_card_numbers")]
        public string CreditCardNumber;
        
        [JsonProperty("yr")]
        public string Year;
        
        [JsonProperty("mos")]
        public string Month;

        public PurchasingForm() {}
        
        public PurchasingForm(string email, string creditCardNumber, string year, string month)
        {
            Email = email;
            CreditCardNumber = creditCardNumber;
            Year = year;
            Month = month;
        }

        public void Clear()
        {
            Month = Year = CreditCardNumber = Email = string.Empty;
        }
    }
}