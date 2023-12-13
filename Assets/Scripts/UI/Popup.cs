using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace DefaultNamespace.UI
{
    public class Popup : MonoBehaviour
    {
        private const float ScaleDuration = 0.3f;
        private const float GlobalPopupTime = 1f;
        private bool isPopupActive = false;
        [SerializeField]
        private TextMeshProUGUI textField;


        public void ShowPopup(string newText)
        {
            // If a popup is already active, close it and open the new one
            if (isPopupActive)
            {
                ClosePopup(() => OpenPopup(newText));
            }
            else
            {
                // If no popup is active, open one
                OpenPopup(newText);
            }
        }

        private void OpenPopup(string newText)
        {
            // Set the text field content
            textField.text = newText;

            // Use DOTween to scale up the popup
            transform.DOScale(Vector3.one, ScaleDuration)
                .SetEase(Ease.OutBack)
                .OnStart(() =>
                {
                    isPopupActive = true;
                    // Start a coroutine to automatically close the popup after the global popup time
                    StartCoroutine(AutoCloseCoroutine());
                });
        }

        private void ClosePopup(System.Action onClosed = null)
        {
            // Use DOTween to scale down the popup
            transform.DOScale(Vector3.zero, ScaleDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    // Optionally, you can do something when the popup is closed
                    // For example, destroy the game object
                    isPopupActive = false;

                    // Invoke the callback function after the popup is closed
                    onClosed?.Invoke();
                });
        }

        private IEnumerator AutoCloseCoroutine()
        {
            // Wait for the global popup time
            yield return new WaitForSeconds(GlobalPopupTime);

            // Close the popup after the global popup time
            ClosePopup();
        }
    }
}
