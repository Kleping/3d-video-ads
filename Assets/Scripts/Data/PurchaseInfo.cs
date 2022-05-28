using UnityEngine;

namespace Data
{
    public class PurchaseInfo
    {
        public string Name, Price, CurrencySymbol, Id;
        public Texture Image;

        public PurchaseInfo(string id, string name, string price, string currencySymbol, Texture image)
        {
            Id = id;
            Name = name;
            Price = price;
            CurrencySymbol = currencySymbol;
            Image = image;
        }
    }
}