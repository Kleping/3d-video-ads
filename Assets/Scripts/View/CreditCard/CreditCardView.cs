using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.CreditCard
{
    public class CreditCardView : MonoBehaviour
    {
        [Space(5)]
        [SerializeField] TMP_InputField _inputEmail;
        [SerializeField] TMP_InputField _inputCreditCardNumber;
        [SerializeField] TMP_InputField _inputCreditYear;
        [SerializeField] TMP_InputField _inputCreditMonth;

        [Space(5)]
        [SerializeField] Button _buttonConfirm;
        [SerializeField] GameObject _objBlocker;
        
        readonly ICreditCardController _controller = new CreditCardController();


        public void Initialize()
        {
            gameObject.SetActive(true);
        }

        public void OnInputChange()
        {
            _buttonConfirm.interactable = validateForm();
        }
        
        public void OnCloseClick()
        {
            clear();
        }

        public async void OnConfirmClick()
        {
            _objBlocker.SetActive(true);
            var succeed = await _controller.ConfirmPurchasing();
            if (succeed) clear();
            _objBlocker.SetActive(false);
        }
        
        
        void clear()
        {
            _inputEmail.SetTextWithoutNotify(string.Empty);
            _inputCreditCardNumber.SetTextWithoutNotify(string.Empty);
            _inputCreditYear.SetTextWithoutNotify(string.Empty);
            _inputCreditMonth.SetTextWithoutNotify(string.Empty);
            _buttonConfirm.interactable = false;
            gameObject.SetActive(false);
        }

        bool validateForm()
        {
            try
            {
                var email = _inputEmail.text;
                var creditCardNumber = _inputCreditCardNumber.text;
                var year = _inputCreditYear.text;
                var month = _inputCreditMonth.text;
                return _controller.ValidateForm(email, creditCardNumber, year, month);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            return false;
        }
    }
}