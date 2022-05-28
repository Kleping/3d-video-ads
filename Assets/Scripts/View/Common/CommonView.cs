using UnityEngine;
using UnityEngine.UI;
using View.Product;

namespace View.Common
{
    public class CommonView : MonoBehaviour
    {
        [Space(5)]
        [SerializeField] ProductView _viewProduct;

        [Space(5)]
        [SerializeField] Button _buttonPurchase;
        
        readonly ICommonController _controller = new CommonController();
        

        public async void OnPurchaseClick()
        {
            _buttonPurchase.interactable = false;
            var purchaseInfo = await _controller.InitiatePurchasing();
            _viewProduct.Initialize(purchaseInfo);
            _buttonPurchase.interactable = true;
        }
    }
}
