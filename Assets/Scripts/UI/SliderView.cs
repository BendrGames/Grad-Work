using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderView : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI numberText;

    void Start()
    {
        // Set the initial value of the number
        UpdateNumberText();

        // Add a listener to the slider to detect changes
        slider.onValueChanged.AddListener(delegate { UpdateNumberText(); });
    }

    // Update the number text based on the slider value
    void UpdateNumberText()
    {
        float sliderValue = slider.value;
        numberText.text = Mathf.Round(sliderValue).ToString();
    }
    
    public string GetNumber()
    {
        float sliderValue = slider.value;
       return Mathf.Round(sliderValue).ToString();
    }
    
    public void SetNumber(int number)
    {
        slider.value = number;
    }
}
