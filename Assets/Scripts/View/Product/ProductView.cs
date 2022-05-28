using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.CreditCard;

namespace View.Product
{
    public class ProductView : MonoBehaviour
    {
        [Space(5)]
        [SerializeField] CreditCardView _creditCardView;
        
        [Space(5)]
        [SerializeField] TMP_Text _textName;
        [SerializeField] TMP_Text _textPrice;
        
        [Space(5)]
        [SerializeField] RawImage _image;

        // For the future functional scalability.
        readonly IProductController _controller = new ProductController();
        string _id;
        // end
        
        
        public void Initialize(PurchaseInfo purchaseInfo)
        {
            _id = purchaseInfo.Id;
            _textName.text = purchaseInfo.Name;
            _textPrice.text = $"{purchaseInfo.Price} {purchaseInfo.CurrencySymbol}";
            _image.texture = purchaseInfo.Image;
            gameObject.SetActive(true);
        }

        void clear()
        {
            _textPrice.text = _textName.text = string.Empty;
            _image.texture = default;
            gameObject.SetActive(false);
        }
        

        public void OnBuyClick()
        {
            _creditCardView.Initialize();
            clear();
        }

        public void OnCloseClick()
        {
            clear();
        }
    }
}