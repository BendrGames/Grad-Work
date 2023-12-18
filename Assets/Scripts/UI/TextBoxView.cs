using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
namespace DefaultNamespace.UI
{
    public class TextBoxView : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField inputField;

        [SerializeField]
        private List<string> funnyAiGuesses = new List<string>();
        bool valuechanged = false;

        private void Start()
        {
            inputField.onValueChanged.AddListener(OnInputValueChanged);
        }
        private void OnInputValueChanged(string arg0)
        {
            valuechanged = true;
        }

        public void resetValueChanged()
        {
            valuechanged = false;
        }

        public void SetInputFieldText(string text)
        {
            inputField.text = text;
        }

        public void setFunnyAiGuess()
        {
            int randomGuess = Random.Range(0, funnyAiGuesses.Count);

            inputField.placeholder.GetComponent<TMP_Text>().text = funnyAiGuesses[randomGuess];
        }

        public string GetInputFieldTextIfValueChanged()
        {
            if (valuechanged)
            {
                return inputField.text;
            }
            else
            {
                return "No Answer, filter this out Ben";
            }
        }

    }
}
