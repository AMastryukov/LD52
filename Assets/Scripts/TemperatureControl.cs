using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureControl : MonoBehaviour
{
    private const float minTemp = 18;
    private const float maxTemp = 26;

    public float CurrentTemperature { get; private set; } = 22f;

    public void IncreaseTemperature()
    {
        CurrentTemperature = Mathf.Clamp(CurrentTemperature - 0.5f, minTemp, maxTemp);
    }

    public void DecreaseTemperature()
    {
        CurrentTemperature = Mathf.Clamp(CurrentTemperature + 0.5f, minTemp, maxTemp);
    }
}
