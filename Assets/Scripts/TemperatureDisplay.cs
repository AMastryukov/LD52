using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemperatureDisplay : MonoBehaviour
{
    [SerializeField] private TemperatureControl temperatureControl;
    [SerializeField] private TextMeshProUGUI temperatureText;

    private void Update()
    {
        var temperatureFormatted = string.Format("{0:0.0}", temperatureControl.CurrentTemperature);
        temperatureText.text = $"{temperatureFormatted}<sup>°C</sup>";
    }
}
