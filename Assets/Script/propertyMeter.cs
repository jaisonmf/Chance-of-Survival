using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This script calculates the values taken away/added to meters in PlayerController.cs

public class propertyMeter : MonoBehaviour
{

    public Slider meterSlider;

    public void UpdateMeter(float Currentvalue, float maxValue)
    {
        float percentageResult = Currentvalue / maxValue;
        meterSlider.value = percentageResult;
    }
}
